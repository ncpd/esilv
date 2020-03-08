var express = require("express");
var router = express.Router();
const db = require("../middlewares/db");

/* GET deptNo employees */
router.get("/:deptNo/employees", function(req, res, next) {
  if (req.query.pageNo > 0 || req.query.pageNo !== 0) {
    /* on matche sur le département actuel, on renvoie le n° d'employé, nom et prénom et on le pagine */
    db.find(
      { "current_dept.dept_no": req.params.deptNo },
      { _id: 0, emp_no: 1, first_name: 1, last_name: 1 },
      req.query.pageNo,
      req.query.size,
      (err, results) => {
        if (err) throw err;
        res.json({ data: results });
      }
    );
  } else {
    res.status(400).json({ message: "Invalid Page" });
  }
});

/* GET deptNo managers */
router.get("/:deptNo/managers", function(req, res, next) {
  opMatch = { $match: { "current_dept.dept_no": req.params.deptNo } }; // match sur le département
  opUnwind = { $unwind: "$titles" }; // unwind des titres
  opProject = {
    $project: {
      emp_no: 1,
      currentDept: "$current_dept",
      firstName: "$first_name",
      lastName: "$last_name",
      toDateYear: { $year: "$titles.to_date" },
      isManager: "$titles.isManager"
    }
  }; // projection pour obtenir l'année de chaque titre
  opMatch2 = { $match: { toDateYear: { $eq: 9999 }, isManager: true } }; // match sur 9999 (=> année actuelle) et si l'employé est manager
  db.aggregate([opMatch, opUnwind, opProject, opMatch2], (err, results) => {
    if (err) throw err;
    res.json({ data: results });
  });
});

/* GET deptNo latest employees */
router.get("/:deptNo/employees/latest", function(req, res, next) {
  opMatch = {
    $match: {
      "current_dept.dept_no": req.params.deptNo,
      "current_dept.from_date": {
        $gte: new Date("2002-01-13T00:00:00.000+0000")
      }
    }
  }; // match sur le département et la date max des derniers arrivés
  opProject = { $project: { first_name: 1, last_name: 1 } }; // projection du nom et prénom
  opSort = { $sort: { "current_dept.from_date": -1 } }; // sort par date pour les derniers arrivés
  db.aggregate([opMatch, opProject, opSort], (err, results) => {
    if (err) throw err;
    res.json({ data: results });
  });
});

/* GET gender stats */
router.get("/stats/gender", function(req, res, next) {
  var requestManagers = req.query.type === "manager";
  if (requestManagers) {
    mapFunction = function() {
      emp_no = this.emp_no;
      for (i = 0; i < this.titles.length; i++) {
        if (
          this.titles[i].to_date.getYear() + 1900 === 9999 &&
          this.titles[i].isManager
        ) {
          emit(this.gender, { nb: 1 });
        }
      }
    }; // on regarde si l'employé est manager actuellement, et si oui, on emit son genre
    reduceFunction = function(key, values) {
      return { nb: values.length };
    }; // on reduce sur le nombre de M et de F
  } else {
    mapFunction = function() {
      if (this.gender === "M") {
        emit(this.current_dept.dept_name, { M: 1, F: 0, nb: 1 });
      } else {
        emit(this.current_dept.dept_name, { M: 0, F: 1, nb: 1 });
      }
    }; // on emit le genre de chaque employé
    reduceFunction = function(key, values) {
      sumM = 0;
      sumF = 0;
      nb = 0;
      for (i = 0; i < values.length; i++) {
        sumM += values[i].M;
        sumF += values[i].F;
        nb += values[i].nb;
      }
      return { M: sumM, F: sumF, nb: nb };
    }; // on obtient le total par département
  }
  queryParam = { query: {}, out: { inline: true } };
  db.mapReduce(mapFunction, reduceFunction, queryParam, (err, docs) => {
    var results = [];
    docs.forEach(element => {
      if (requestManagers) {
        results.push({
          gender: element._id,
          tot: element.value.nb
        });
      } else {
        results.push({
          department: element._id,
          M: Math.round((element.value.M / element.value.nb) * 100), // pourcentage d'hommes
          F: Math.round((element.value.F / element.value.nb) * 100), // pourcentage de femmes
          tot: element.value.nb
        });
      }
    });
    res.json({ data: results });
  });
});

/* GET salary stats */
router.get("/stats/salary", function(req, res, next) {
  mapFunction = function() {
    sum = 0;
    nb = 0;
    for (i = 0; i < this.salaries.length; i++) {
      sum += this.salaries[i].salary;
      nb++;
    }
    emit(this.current_dept.dept_name, { sum: sum, nb: nb });
  }; // on emit seulement la somme des salaires et le nb de salaires (réduit le nb d'emit)
  reduceFunction = function(key, values) {
    sum = 0;
    nb = 0;
    for (i = 0; i < values.length; i++) {
      sum += values[i].sum;
      nb += values[i].nb;
    }
    return { avg: sum / nb, sum: sum, nb: nb };
  }; // calcul de moyenne à partir des valeurs précédentes
  queryParam = { query: {}, out: { inline: true } };
  db.mapReduce(mapFunction, reduceFunction, queryParam, (err, docs) => {
    var results = [];
    docs.forEach(element => {
      results.push({
        department: element._id,
        salary: Math.round(element.value.avg * 100) / 100
      });
    });
    res.json({ data: results });
  });
});

module.exports = router;
