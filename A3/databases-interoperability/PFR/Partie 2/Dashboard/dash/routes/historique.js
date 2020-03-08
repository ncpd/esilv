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

function findClients(req, res, next) {
    dbRequest = 'SELECT codeC, nom, prenom, email FROM client';
    db.query(dbRequest, function(error, rows) {
        /* Add selected data to previous saved data. */
        req.clients = rows;
        next();
    });
}

function findVoitures(req, res, next) {
    dbRequest = 'SELECT immat, marque, modele, categorie, disponibilite, p.nom, adresse, ville, place_parking from voiture v inner join parking p on p.id = v.id_parking;';
    db.query(dbRequest, function(error, rows) {
        /* Add selected data to previous saved data. */
        req.voitures = rows;
        next();
    });
}

function renderHistoriquePage(req, res) {
    res.render('historique', {
        title: 'Escapade - Dashboard',
        clients: req.clients,
        voitures: req.voitures
    });
}

function renderClientSearchPage(req, res) {
    console.log("Here");
    res.render('histo_client', {
        title: 'Escapade - Dashboard',
        clients: req.clients,
        voitures: req.voitures
    });
}

function findInterventions(req, res, next) {
    if(req.query.immat !== undefined) {
        dbRequest = 'SELECT id, id_controleur, type_intervention, date FROM intervention WHERE immat = \'' + req.query.immat.toUpperCase() + "\'";
        db.query(dbRequest, function (error, rows) {
            /* Add selected data to previous saved data. */
            req.inter = rows;
            console.log(rows);
            next();
        });
    } else {
        res.render('histo_vehicule', {
            title: 'Escapade - Dashboard véhicule'
        })
    }
}

/* GET listing. */
router.get('/', findClients, findVoitures, renderHistoriquePage);

router.get('/client', renderClientSearchPage);

router.get('/vehicule', findInterventions, function (req, res, next) {
    //res.send(req.query.immat);
    if(req.query.immat !== undefined) {
        var immat = req.query.immat.toUpperCase();
        res.render('histo_vehicule', {
            title: 'Escapade - Dashboard - ' + immat,
            inter: req.inter
        })
    } else {
        res.render('histo_vehicule', {
            title: 'Escapade - Dashboard véhicule'
        })
    }
});

module.exports = router;