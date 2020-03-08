var express = require("express");
var router = express.Router();
const db = require("../middlewares/db");

/* GET employees from title */
router.get("/", function(req, res, next) {
  title = req.query.title;
  mapFunction = function() {
    emp_no = this.emp_no;
    for (i = 0; i < this.titles.length; i++) {
      if (this.titles[i].to_date.getYear() + 1900 === 9999) {
        emit(this.titles[i].title, { emp_no: [emp_no] });
      }
    }
  }; // on emit le titre de l'employé avec son numéro d'employé
  reduceFunction = function(key, values) {
    emps = [];
    for (i = 0; i < values.length; i++) {
      for (j = 0; j < values[i].emp_no.length; j++) {
        emps.push(values[i].emp_no[j]);
      }
    }
    return { emp_no: emps };
  }; // on récupère les numéros d'employés
  queryParam = { query: {}, out: { inline: true } };
  db.mapReduce(mapFunction, reduceFunction, queryParam, (err, docs) => {
    var data = [];
    docs.forEach(element => {
      if (element._id === title) {
        data = element; // récupération des données qui nous intéressent uniquement (titre demandé)
      }
    });
    res.json({ data: data });
  });
});

/* GET number employees per titles */
router.get("/titles", function(req, res, next) {
  mapFunction = function() {
    emp_no = this.emp_no;
    for (i = 0; i < this.titles.length; i++) {
      if (this.titles[i].to_date.getYear() + 1900 === 9999) {
        emit(this.titles[i].title, { nb: 1 });
      }
    }
  }; // on emit le titre et 1 pour compter les employés ayant le même titre
  reduceFunction = function(key, values) {
    nb = 0;
    for (i = 0; i < values.length; i++) {
      nb += values[i].nb;
    }
    return { nb: nb };
  }; // on retourne le nombre d'employés
  queryParam = { query: {}, out: { inline: true } };
  db.mapReduce(mapFunction, reduceFunction, queryParam, (err, docs) => {
    var data = docs.map(element => {
      return { title: element._id, nb: element.value.nb };
    });
    res.json({ data: data });
  });
});

/* GET random employee */
router.get("/random", (req, res, next) => {
  var opUnwind = { $unwind: "$salaries" }; // on unwind les salaires
  var opProject = {
    $project: {
      emp_no: 1,
      toDateYear: { $year: "$salaries.to_date" },
      amount: "$salaries.salary",
      current_dept: "$current_dept"
    }
  }; // on projette pour obtenir l'année
  var opMatch = { $match: { toDateYear: { $eq: 9999 } } }; // on match sur l'année actuelle (=> 9999)
  var opSample = { $sample: { size: 1 } }; // on sample pour obtenir un employé aléatoire
  db.aggregate([opUnwind, opProject, opMatch, opSample], (err, results) => {
    if (err) throw err;
    res.json({ data: results });
  });
});

/* GET employee salary */
router.get("/:empNo/salaries/last", function(req, res, next) {
  var opMatch = { $match: { emp_no: parseInt(req.params.empNo) } }; // on match sur le numéro d'employé
  var opUnwind = { $unwind: "$salaries" }; // on unwind ses salaires
  var opProject = {
    $project: {
      emp_no: 1,
      current_dept: 1,
      toDateYear: { $year: "$salaries.to_date" },
      first_name: 1,
      last_name: 1,
      amount: "$salaries.salary"
    }
  }; // on projette pour obtenir l'année de chaque salaire et le salaire en question
  var opMatch2 = { $match: { toDateYear: { $eq: 9999 } } }; // on match sur l'année actuelle (=> 9999)
  db.aggregate([opMatch, opUnwind, opProject, opMatch2], (err, results) => {
    if (err) throw err;
    res.json({ data: results });
  });
});

/* GET employee salaries */
router.get("/:empNo/salaries", function(req, res, next) {
  // on renvoie tous les salaires d'un employé
  db.find(
    { emp_no: parseInt(req.params.empNo) },
    { salaries: 1 },
    null,
    null,
    (err, data) => {
      if (err) throw err;
      res.json({ data });
    }
  );
});

module.exports = router;
