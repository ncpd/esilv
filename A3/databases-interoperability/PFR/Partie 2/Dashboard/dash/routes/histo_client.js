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

function findLocations(req, res, next) {
    dbRequest = 'SELECT id, sejour, immat, appreciation, note FROM location WHERE codeC =\'C' + req.params.id + '\'' ;
    db.query(dbRequest, function(error, rows) {
        /* Add selected data to previous saved data. */
        req.locations = rows;
        console.log(rows);
        next();
    });
}

function renderHistoriquePage(req, res) {
    res.render('histo_client', {
        title: 'Escapade - Historique Client',
        locations: req.locations,
    });
}

function renderHistoriqueWithIdPage(req, res) {
    console.log("In client page");
    //res.send('user ' + req.params.id);
    res.render('histo_client', {
        title: 'Escapade - Historique Client C' + req.params.id,
        locations: req.locations,
    });
}

/* GET listing. */
router.get('/historique/client/', findLocations, renderHistoriquePage);

router.get('/:id', findLocations, renderHistoriqueWithIdPage);

module.exports = router;