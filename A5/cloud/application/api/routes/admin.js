var express = require("express");
var router = express.Router();
const db = require("../middlewares/db");

/**
 * Returns (formatted) stats information about the collection
 */
router.get("/stats", function(req, res) {
  db.stats({}, (err, stats) => {
    if (err) throw err;
    results = {
      count: stats.count,
      size: stats.count,
      storageSize: stats.storageSize,
      totalIndexSize: stats.totalIndexSize,
      indexSizes: stats.indexSizes,
      avgObjSize: stats.avgObjSize,
      nchunks: stats.nchunks,
      nindexes: stats.nindexes
    };
    shards = [];
    shardsNames = Object.keys(stats.shards).sort();
    shardsNames.forEach(element => {
      shards.push({
        name: element,
        count: stats.shards[element].count,
        avgObjSize: stats.shards[element].avgObjSize,
        storageSize: stats.shards[element].storageSize,
        ok: stats.shards[element].ok
      });
    });
    results.shards = shards;
    res.json({ data: results });
  });
});

/**
 * Returns shards information
 */
router.get("/shards", function(req, res) {
  db.adminCmd("listShards", (err, result) => {
    if (err) throw err;
    res.json({ data: result });
  });
});

/**
 * Returns chunks repartition
 */
router.get("/chunks", function(req, res) {
  var opMatch = { $match: { ns: "employee.employees" } };
  var opGroup = { $group: { _id: "$shard", count: { $sum: 1 } } };
  db.config([opMatch, opGroup], (err, result) => {
    if (err) throw err;
    res.json({ data: result });
  });
});

/**
 * Retuns chunks repartition details with ranges and keys
 */
router.get("/chunks/details", function(req, res) {
  var opMatch = { $match: { ns: "employee.employees" } };
  var opProject = {
    $project: { _id: 1, shard: "$shard", min: "$min", max: "$max" }
  };
  db.config([opMatch, opProject], (err, result) => {
    if (err) throw err;
    res.json({ data: result });
  });
});

/**
 * Return server status information
 */
router.get("/status", function(req, res) {
  db.adminCmd("serverStatus", (err, result) => {
    if (err) throw err;
    res.json({ data: result });
  });
});

/**
 * Returns connections stats information
 */
router.get("/connections", function(req, res) {
  db.adminCmd("connPoolStats", (err, result) => {
    if (err) throw err;
    res.json({ data: result });
  });
});

/**
 * Returns stats about the host (mongos)
 */
router.get("/host", function(req, res) {
  db.adminCmd("hostInfo", (err, result) => {
    if (err) throw err;
    res.json({ data: result });
  });
});

module.exports = router;
