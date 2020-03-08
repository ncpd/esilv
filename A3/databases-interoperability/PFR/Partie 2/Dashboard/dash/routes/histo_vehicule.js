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

function findInters(req, res, next) {
    dbRequest = 'SELECT * FROM intervention WHERE IMMAT = \'\';';
    db.query(dbRequest, function(error, rows) {
        /* Add selected data to previous saved data. */
        req.voitures = rows;
        next();
    });
}

function renderHistoriquePage(req, res) {
    console.log("Dans histo_vehicule.js");
    console.log("params : " + req.params);
    res.render('histo_vehicule', {
        title: 'Escapade - Historique Véhicule',
        //locations: req.locations,
    });
}

function renderHistoriqueWithIdPage(req, res) {
    var immat = req.query.immat;
    console.log("Dans histo_vehicule.js AVEC ID");
    console.log("params id : " + req.params);
    console.log("immat : " + immat);
    res.send('car immat : ' + req.params);
    res.render('histo_vehicule', {
        title: 'Escapade - Historique Véhicule' + req.params.id,
        locations: req.locations,
    });
}

/* GET listing. */
//router.get('/historique/vehicule', renderHistoriquePage);

router.get('/historique/vehicule', renderHistoriqueWithIdPage);

module.exports = router;