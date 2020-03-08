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

function countlocations(req, res, next) {
    if(req.query.immat !== undefined) {
        var dbRequest = 'SELECT count(*) as nb_locations FROM location WHERE immat = \'' + req.query.immat.toUpperCase() + '\'';
        db.query(dbRequest, function (error, rows) {
            if (rows.length !== 0) {
                req.locs = rows[0].nb_locations;
                console.log(req.locs);
                return next();
            }

            res.render('rentabilite', {
                title: 'Escapade - Rentabilité',
                nb_locations: 0,
                avis_moyen: 0,
                locs_yearly: 0
            });
            /* Render the error page. */
        });
    } else {
        res.render('rentabilite', {
            title: 'Escapade - Rentabilité',
            nb_locations: 0,
            avis_moyen: 0,
            locs_yearly: 0
        });
    }
}

function getAnnualLocs(req, res, next) {
    if(req.query.immat !== undefined) {
        var dbRequest = 'SELECT count(*) as nb_locs from location l inner join sejour s on l.sejour = s.id where immat = \'' + req.query.immat.toUpperCase() + '\' group by annee';
        db.query(dbRequest, function (error, rows) {
            req.locs_yearly = rows;
            next();
        });
    } else {
        res.render('rentabilite', {
            title: 'Escapade - Rentabilité',
            nb_locations: 0,
            avis_moyen: 0,
            locs_yearly: 0
        });
    }
}

function getRating(req, res, next) {
    if(req.query.immat !== undefined) {
        var dbRequest = 'SELECT avg(note) as avg FROM location WHERE immat = \'' + req.query.immat.toUpperCase() + '\'';
        db.query(dbRequest, function (error, rows) {
            if (rows.length !== 0) {
                req.avg = rows[0].avg;
                console.log(req.avg);
                return next();
            }

            res.render('rentabilite', {
                title: 'Escapade - Rentabilité',
                nb_locations: 0,
                avis_moyen: 0,
                locs_yearly: 0
            });
            /* Render the error page. */
        });
    } else {
        res.render('rentabilite', {
            title: 'Escapade - Rentabilité',
            nb_locations: 0,
            avis_moyen: 0,
            locs_yearly: 0
        })
    }
}

router.get('/', countlocations, getAnnualLocs, getRating, function (req, res, next) {
    if(req.query.immat !== undefined) {
        var immat = req.query.immat.toUpperCase();
        res.render('rentabilite', {
            title: 'Escapade - Rentabilité ' + immat,
            nb_locations: req.locs,
            avis_moyen: req.avg,
            locs_yearly: req.locs_yearly,
            immat: immat
        })
    } else {
        res.render('rentabilite', {
            title: 'Escapade - Rentabilité',
            nb_locations: 0,
            avis_moyen: 0,
            locs_yearly: 0
        })
    }
});

module.exports = router;