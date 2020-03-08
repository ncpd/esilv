const MongoClient = require("mongodb").MongoClient;
const url = "mongodb://devincimdb1046.westeurope.cloudapp.azure.com:30000";
const dbName = "employee";
const collName = "employees";

/**
 * Connects to the DB using DB string and returns a MongoClient to the callback
 */
exports.connect = callback => {
  MongoClient.connect(url, function(err, client) {
    if (err) return callback(err, null);
    return callback(null, client);
  });
};

/**
 * Retrieve chunks information about employee.employees collection and returns in into the callback
 */
exports.config = (pipeline, callback) => {
  this.connect((err, client) => {
    if (err) return callback(err, null);
    const db = client.db("config");
    const collection = db.collection("chunks");
    collection.aggregate(pipeline, (err, results) => {
      if (err) return callback(err, null);
      results.toArray((err, docs) => {
        if (err) return callback(err, null);
        return callback(null, docs);
      });
    });
  });
};

/**
 * Util method executing an aggregate from a pipeline and returning its results into the callback
 */
exports.aggregate = (pipeline, callback) => {
  this.connect((err, client) => {
    if (err) return callback(err, null);
    const db = client.db(dbName);
    const collection = db.collection(collName);
    collection.aggregate(pipeline, (err, results) => {
      if (err) return callback(err, null);
      results.toArray((err, docs) => {
        if (err) return callback(err, null);
        return callback(null, docs);
      });
    });
  });
};

/**
 * Util method executing a find with possible paging and projection, and returning its results into the callback
 */
exports.find = (query, projection, pageNo, size, callback) => {
  this.connect((err, client) => {
    if (err) return callback(err, null);
    const db = client.db(dbName);
    const collection = db.collection(collName);
    if(pageNo && size){
      collection
        .find(query, {
          projection: projection,
          skip: parseInt(size * (pageNo - 1)),
          limit: parseInt(size)
        })
        .sort({ emp_no: 1 })
        .toArray((err, docs) => {
          if (err) return callback(err, null);
          return callback(null, docs);
        });
    } else {
      collection
        .find(query, {
          projection
        })
        .sort({ emp_no: 1 })
        .toArray((err, docs) => {
          if (err) return callback(err, null);
          return callback(null, docs);
        });
      }
  });
};

/**
 * Util method executing a mapReduce from map and reduce functions and a query parameter and returning its results into the callback
 */
exports.mapReduce = (mapFunction, reduceFunction, query, callback) => {
  this.connect((err, client) => {
    if (err) return callback(err, null);
    const db = client.db(dbName);
    const collection = db.collection(collName);
    collection.mapReduce(mapFunction, reduceFunction, query, (err, docs) => {
      if (err) return callback(err, null);
      return callback(null, docs);
    });
  });
};

/**
 * Util method retrieving collection stats for a specific collection
 */
exports.stats = (options, callback) => {
  this.connect((err, client) => {
    if (err) return callback(err, null);
    const db = client.db(dbName);
    const collection = db.collection(collName);
    collection.stats(options, (err, stats) => {
      if (err) return callback(err, null);
      return callback(null, stats);
    });
  });
};

/**
 * Util method executing an admin command in the authorized ones
 * Authorized commands : listShards, serverStatus, connPoolStats, hostInfo
 */
exports.adminCmd = (command, callback) => {
  authorized = ["listShards", "serverStatus", "connPoolStats", "hostInfo"];
  if (authorized.includes(command)) {
    this.connect((err, client) => {
      if (err) return callback(err, null);
      const db = client.db(dbName);
      const adminDB = db.admin();
      cmd = {};
      cmd[command] = 1;
      adminDB.command(cmd, (err, result) => {
        if (err) callback(err, null);
        return callback(null, result);
      });
    });
  } else {
    return callback({ message: "Unauthorized command" }, null);
  }
};
