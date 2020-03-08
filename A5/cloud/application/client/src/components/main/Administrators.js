import React from "react";
import { withStyles } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper";
import Grid from "@material-ui/core/Grid";
import { Pie, Bar } from "react-chartjs-2";
import axios from "axios";
import ProgressScreen from "./ProgressScreen";
import Moment from "react-moment";
import { Typography } from "@material-ui/core";
import { config } from "../../config/default";

const host = config.api.host;
const port = config.api.port;

const styles = theme => ({
  root: {
    flexGrow: 1
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: "center",
    color: theme.palette.text.primary,
    backgroundColor: theme.palette.primary.light
  },
  up: {
    backgroundColor: "#248e36 !important",
    color: "#fff"
  },
  down: {
    backgroundColor: "#ff0000",
    color: "#fff"
  },
  secondary: {
    color: theme.palette.secondary.main
  }
});

/**
 * Component holding administrative and servers information
 */
class Administrators extends React.Component {
  constructor() {
    super();
    this.state = {
      loading: false,
      shardsData: [],
      hostStatus: [],
      connectionsData: {
        replicaSets: []
      },
      hostInfo: {
        cpu: {},
        os: {}
      },
      chunks: [],
      chunksDetails: [],
      dbStats: {
        pie: {},
        bar: {}
      }
    };
  }

  /**
   * Parse the response from the host status from API in order to keep proper information
   */
  parseHostStatusResponse = data => {
    var result = {
      host: data.host,
      uptime: data.uptimeMillis,
      ok: data.ok
    };
    return result;
  };

  /**
   * Parse the response from the connections stats from API in order to keep useful information
   */
  parseConnectionsResponse = data => {
    var replicaSetsLabels = Object.keys(data.replicaSets).sort();
    var replicaSets = [];
    replicaSetsLabels.forEach(element => {
      replicaSets.push({
        name: element,
        hosts: data.replicaSets[element].hosts
      });
    });
    var result = {
      clientConnections: data.numClientConnections,
      replicaSets: replicaSets
    };
    return result;
  };

  /**
   * Parse the response from the host info from API in order to keep proper sysinfo
   */
  parseHostInfoResponse = data => {
    var result = {
      memory: data.system.memSizeMB,
      cpu: {
        arch: data.system.cpuArch,
        nbCores: data.system.numCores,
        freq: data.extra.cpuFrequencyMHz
      },
      os: data.os,
      kernel: data.extra.kernelVersion
    };
    return result;
  };

  /**
   * Maps different hosts to their respective replicaSets
   */
  mapReplicaSetHosts = hosts => {
    var arr = [];
    hosts.forEach(host => {
      var masterStr = host.ismaster ? " [master]" : "";
      arr.push(host.addr.split(":")[0] + masterStr);
    });
    return arr.join();
  };

  /**
   * Parse the response from the chunks count from API in order to build the pie chart
   */
  parseChunksResponse = chunks => {
    var labels = chunks.map(chunk => chunk._id);
    var dataset = chunks.map(chunk => chunk.count);
    var data = {
      labels: labels,
      datasets: [
        {
          data: dataset,
          backgroundColor: [
            "#f05a28",
            "#f56f1f",
            "#f7761c",
            "#fc920c",
            "#fcad00",
            "#fac70b"
          ]
        }
      ]
    };
    return data;
  };

  /**
   * Parse the response from the chunks details from API in order to get their respective ranges
   */
  parseChunksDetailsResponse = chunksDetails => {
    var results = [];
    chunksDetails.forEach(chunk => {
      var chunkMinKey = chunk.min["current_dept.dept_no"];
      var chunkMaxKey = chunk.max["current_dept.dept_no"];
      var min, max;
      if (chunkMinKey && typeof chunkMinKey === "string") {
        min = chunkMinKey;
      } else {
        min = chunkMinKey._bsontype;
      }
      if (chunkMaxKey && typeof chunkMaxKey === "string") {
        max = chunkMaxKey;
      } else {
        max = chunkMaxKey._bsontype;
      }
      results.push({
        _id: chunk.shard,
        min: min,
        max: max
      });
    });
    return results;
  };

  /**
   * Parse the response from the stats from API in order to draw pie and bar charts
   */
  parseStatsResponse = stats => {
    var labels = stats.shards.map(shard => shard.name);
    var datasetCount = stats.shards.map(shard => shard.count);
    var datasetAvg = stats.shards.map(shard => shard.avgObjSize);
    var dataPie = {
      labels: labels,
      datasets: [
        {
          data: datasetCount,
          backgroundColor: [
            "#f05a28",
            "#f56f1f",
            "#f7761c",
            "#fc920c",
            "#fcad00",
            "#fac70b"
          ]
        }
      ]
    };
    var dataBar = {
      labels: labels,
      datasets: [
        {
          label: "Size (bytes)",
          backgroundColor: "rgba(240,90,40,0.2)",
          borderColor: "rgba(240,90,40,1)",
          borderWidth: 1,
          hoverBackgroundColor: "rgba(240,90,40,0.4)",
          hoverBorderColor: "rgba(240,90,40,1)",
          data: datasetAvg
        }
      ]
    };
    return {
      pie: dataPie,
      bar: dataBar
    };
  };

  /**
   * Make concurrent requests into Promises to fetch API info and set it to component state
   */
  componentDidMount() {
    this.setState({
      loading: true
    });
    Promise.all([
      axios.get(`http://${host}:${port}/admin/shards`),
      axios.get(`http://${host}:${port}/admin/status`),
      axios.get(`http://${host}:${port}/admin/connections`),
      axios.get(`http://${host}:${port}/admin/host`),
      axios.get(`http://${host}:${port}/admin/chunks`),
      axios.get(`http://${host}:${port}/admin/chunks/details`),
      axios.get(`http://${host}:${port}/admin/stats`)
    ]).then(
      ([
        shardsResponse,
        hostStatusResponse,
        connectionsResponse,
        hostInfoResponse,
        chunksResponse,
        chunksDetailsResponse,
        statsResponse
      ]) => {
        this.setState({
          shardsData: shardsResponse.data.data.shards,
          hostStatus: this.parseHostStatusResponse(
            hostStatusResponse.data.data
          ),
          connectionsData: this.parseConnectionsResponse(
            connectionsResponse.data.data
          ),
          hostInfo: this.parseHostInfoResponse(hostInfoResponse.data.data),
          chunks: this.parseChunksResponse(chunksResponse.data.data),
          chunksDetails: this.parseChunksDetailsResponse(
            chunksDetailsResponse.data.data
          ),
          dbStats: this.parseStatsResponse(statsResponse.data.data),
          loading: false
        });
      }
    );
  }

  render() {
    const { classes } = this.props;
    var isHostUp = this.state.hostStatus.ok === 1;
    var classUpDown = isHostUp ? classes.up : classes.down;

    return this.state.loading ? (
      <ProgressScreen />
    ) : (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={6}>
            <Grid container spacing={3}>
              {this.state.shardsData.map(element => {
                var isUp = element.state === 1;
                var up = isUp ? "UP" : "DOWN";
                return (
                  <Grid item xs={4}>
                    <Paper className={`${classes.paper} ${classUpDown}`}>
                      <strong>{element.host}</strong>
                      <h1>{up}</h1>
                    </Paper>
                  </Grid>
                );
              })}
              <Grid item xs={6}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>
                    DOCUMENTS REPARTITION
                  </strong>
                  <Pie
                    data={this.state.dbStats.pie}
                    options={{ legend: { labels: { fontColor: "#9f9f9f" } } }}
                  />
                </Paper>
              </Grid>
              <Grid item xs={6}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>
                    AVERAGE OBJECT SIZE PER SHARD
                  </strong>
                  <Bar
                    data={this.state.dbStats.bar}
                    options={{
                      legend: {
                        labels: { fontColor: "#9f9f9f" }
                      }
                    }}
                  />
                </Paper>
              </Grid>
              <Grid item xs={12}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>REPLICASETS</strong>
                  <Grid container spacing={1}>
                    {this.state.connectionsData.replicaSets.map(element => {
                      return (
                        <Grid item xs={6}>
                          <Typography>
                            <strong className={classes.secondary}>
                              {element.name}
                            </strong>{" "}
                            [{this.mapReplicaSetHosts(element.hosts)}]
                          </Typography>
                        </Grid>
                      );
                    })}
                  </Grid>
                </Paper>
              </Grid>
            </Grid>
          </Grid>
          <Grid item xs={6}>
            <Grid container spacing={3}>
              <Grid item xs={4}>
                <Paper className={`${classes.paper} ${classUpDown}`}>
                  <strong>{this.state.hostStatus.host}</strong>
                  <h1>{isHostUp ? "UP" : "DOWN"}</h1>
                </Paper>
              </Grid>
              <Grid item xs={4}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>UPTIME</strong>
                  <h1>
                    <Moment format="hh:mm">
                      {this.state.hostStatus.uptime}
                    </Moment>
                  </h1>
                </Paper>
              </Grid>
              <Grid item xs={4}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>
                    CLIENT CONNECTIONS
                  </strong>
                  <h1>{this.state.connectionsData.clientConnections}</h1>
                </Paper>
              </Grid>
              <Grid item xs={4}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>MONGOS MEMORY</strong>
                  <h1>{this.state.hostInfo.memory}MB</h1>
                </Paper>
              </Grid>
              <Grid item xs={8}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>MONGOS CPU INFO</strong>
                  <h2>
                    {this.state.hostInfo.cpu.arch}
                    {" / "}
                    {this.state.hostInfo.cpu.nbCores}-Core@
                    {this.state.hostInfo.cpu.freq}MHz
                  </h2>
                </Paper>
              </Grid>
              <Grid item xs={5}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>
                    OPERATING SYSTEM
                  </strong>
                  <h2>
                    {this.state.hostInfo.os.name}{" "}
                    {this.state.hostInfo.os.version}
                  </h2>
                  <h2>{this.state.hostInfo.kernel} kernel</h2>
                </Paper>
              </Grid>
              <Grid item xs={7}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>CHUNKS DETAILS</strong>
                  <Grid container spacing={1}>
                    {this.state.chunksDetails.map(element => {
                      return (
                        <Grid item xs={6}>
                          <Typography>
                            <strong className={classes.secondary}>
                              {element._id}
                            </strong>{" "}
                            [{element.min}{<> &rarr; </>}{element.max}]
                          </Typography>
                        </Grid>
                      );
                    })}
                  </Grid>
                </Paper>
              </Grid>
              <Grid item xs={8}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>
                    CHUNKS REPARTITION
                  </strong>
                  <Pie
                    data={this.state.chunks}
                    options={{ legend: { labels: { fontColor: "#9f9f9f" } } }}
                  />
                </Paper>
              </Grid>
            </Grid>
          </Grid>
        </Grid>
      </div>
    );
  }
}

export default withStyles(styles)(Administrators);
