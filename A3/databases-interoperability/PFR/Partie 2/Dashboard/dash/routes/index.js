var express = require('express');
var mysql = require('mysql');
var router = express.Router();

// Create connection
const db = mysql.createConnection({
    host     : 'fboisson.ddns.net',
    user     : 'S6-PICA-NICO',
    password : '8430',
    database : 'pica_nico'
});

// Connect
db.connect((err) => {
    if(err){
        throw err;
    }
    console.log('MySql Connected...');
});

function countClients(req, res, next) {
    var dbRequest = 'SELECT count(*) as nb_users FROM client;';
    db.query(dbRequest, function(error, rows) {
        if(rows.length !== 0) {
            req.nbClients = rows[0].nb_users;
            console.log(req.nbClients);
            return next();
        }

        res.render('no_users'); /* Render the error page. */
    });
}

function countCars(req, res, next) {
    var dbRequest = 'SELECT count(*) as nb_cars FROM voiture;';
    db.query(dbRequest, function(error, rows) {
        if(rows.length !== 0) {
            req.nbVoitures = rows[0].nb_cars;
            console.log(req.nbVoitures);
            return next();
        }

        res.render('no_cars'); /* Render the error page. */
    });
}

function countBerlines(req, res, next) {
    var dbRequest = 'SELECT count(*) AS nb_berlines FROM location l INNER JOIN voiture v ON l.immat = v.immat WHERE v.categorie = \'berline\';';
    db.query(dbRequest, function(error, rows) {
        if(rows.length !== 0) {
            req.nbBerlines = rows[0].nb_berlines;
            console.log(req.nbBerlines);
            return next();
        }

        res.render('no_berlines'); /* Render the error page. */
    });
}

function countCabriolets(req, res, next) {
    var dbRequest = 'SELECT count(*) AS nb_cabriolets FROM location l INNER JOIN voiture v ON l.immat = v.immat WHERE v.categorie = \'cabriolet\';';
    db.query(dbRequest, function(error, rows) {
        if(rows.length !== 0) {
            req.nbCabriolets = rows[0].nb_cabriolets;
            console.log(req.nbCabriolets);
            return next();
        }

        res.render('no_berlines'); /* Render the error page. */
    });
}

function countlocations(req, res, next) {
    var dbRequest = 'SELECT count(*) as nb_locations FROM location;';
    db.query(dbRequest, function(error, rows) {
        if(rows.length !== 0) {
            req.nbLocations = rows[0].nb_locations;
            console.log(req.nbLocations);
            return next();
        }

        res.render('no_locs'); /* Render the error page. */
    });
}

function findClients(req, res, next) {
    dbRequest = 'SELECT codeC, nom, prenom, email FROM client';
    db.query(dbRequest, function(error, rows) {
        /* Add selected data to previous saved data. */
        req.clients = rows;
        next();
    });
}

function countLocsYearly(req, res, next) {
    dbRequest = 'SELECT count(*) as nb_locs from location l inner join sejour s on s.id = l.sejour group by annee order by annee';
    db.query(dbRequest, function(error, rows) {
        /* Add selected data to previous saved data. */
        req.locs_yearly = rows;
        next();
    });
}

function GetLatestComs(req, res, next) {
    dbRequest = 'SELECT l.appreciation, l.note FROM( SELECT * FROM location WHERE appreciation is not null ORDER BY id DESC LIMIT 3) l ORDER BY l.id ASC';
    db.query(dbRequest, function(error, rows) {
        /* Add selected data to previous saved data. */
        req.coms = rows;
        next();
    });
}

function getAvgRating(req, res, next) {
    dbRequest = 'select avg(note) as avg_rating from location';
    db.query(dbRequest, function(error, rows) {
        /* Add selected data to previous saved data. */
        req.avgRating = rows[0].avg_rating;
        next();
    });
}

function renderStudentsPage(req, res) {
    res.render('index', {
        title: 'Escapade - Dashboard',
        clients: req.clients,
        nb_clients: req.nbClients,
        nb_voitures: req.nbVoitures,
        nb_locations: req.nbLocations,
        nb_cabriolets: req.nbCabriolets,
        nb_berlines: req.nbBerlines,
        locs_yearly: req.locs_yearly,
        avg_rate: req.avgRating,
        coms: req.coms
    });
}

router.get('/', countClients, findClients, countCars, countlocations, countCabriolets, countBerlines, countLocsYearly, getAvgRating, GetLatestComs, renderStudentsPage);
/* GET home page.
router.get('/', function(req, res, next) {
    res.render('index', { title: 'Escapade - Dashboard', clients: result });
}); */

module.exports = router;
