import React from "react";
import { withStyles } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper";
import Grid from "@material-ui/core/Grid";
import { Doughnut, Radar, Bar } from "react-chartjs-2";
import axios from "axios";
import ProgressScreen from "../main/ProgressScreen";
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
    color: theme.palette.text.secondary,
    backgroundColor: theme.palette.primary.light
  }
});

class Analyst extends React.Component {
  constructor() {
    super();
    this.state = {
      loading: false
    };
  }

  parseGenderData = data => {
    var results = {
      labels: [],
      datasets: [
        {
          data: [],
          backgroundColor: "rgba(240,90,40,0.2)",
          borderColor: "rgba(240,90,40,1)",
          pointBackgroundColor: "rgba(240,90,40,1)",
          pointBorderColor: "#fff",
          pointHoverBackgroundColor: "#fff",
          pointHoverBorderColor: "rgba(240,90,40,1)"
        },
        {
          data: [],
          backgroundColor: "rgba(250,199,11,0.2)",
          borderColor: "rgba(250,199,11,1)",
          pointBackgroundColor: "rgba(250,199,11,1)",
          pointBorderColor: "#fff",
          pointHoverBackgroundColor: "#fff",
          pointHoverBorderColor: "rgba(250,199,11,1)"
        }
      ]
    };
    results.datasets[0].label = "Hommes";
    results.datasets[1].label = "Femmes";
    var maleData = [];
    var femaleData = [];
    data.forEach(element => {
      if (element.department !== "No longer employed") {
        results.labels.push(element.department);
        maleData.push(element.M);
        femaleData.push(element.F);
      }
    });
    results.datasets[0].data = maleData;
    results.datasets[1].data = femaleData;
    return results;
  };

  parseManagerData = data => {
    var results = {
      labels: [],
      datasets: [
        {
          data: [],
          borderColor: "#fff",
          backgroundColor: ["rgba(240,90,40,1)", "rgba(250,199,11,1)"],
          pointBorderColor: "#fff",
          pointHoverBackgroundColor: "#fff"
        }
      ]
    };
    data.forEach(element => {
      if (element.gender === "M") {
        results.labels.push("Male");
        results.datasets[0].data.push(element.tot);
      } else {
        results.labels.push("Female");
        results.datasets[0].data.push(element.tot);
      }
    });
    return results;
  };

  parseTitlesData = data => {
    var results = {
      labels: [],
      datasets: [
        {
          label: "Nombre d'employés",
          backgroundColor: "rgba(250,199,11,0.2)",
          borderColor: "rgba(250,199,11,1)",
          borderWidth: 1,
          hoverBackgroundColor: "rgba(250,199,11,0.4)",
          hoverBorderColor: "rgba(250,199,11,1)",
          data: []
        }
      ]
    };
    data.forEach(element => {
      results.labels.push(element.title);
      results.datasets[0].data.push(element.nb);
    });
    return results;
  };

  parseSalaryData = data => {
    var labels = data.map(dept => dept.department);
    var dataset = data.map(dept => dept.salary);
    return {
      labels: labels,
      datasets: [
        {
          label: "Salaire moyen",
          data: dataset,
          backgroundColor: "rgba(240,90,40,0.2)",
          borderColor: "rgba(240,90,40,1)",
          pointBackgroundColor: "rgba(240,90,40,1)",
          pointBorderColor: "#fff",
          pointHoverBackgroundColor: "#fff",
          pointHoverBorderColor: "rgba(240,90,40,1)"
        }
      ]
    };
  };

  componentDidMount() {
    this.setState({
      loading: true
    });
    Promise.all([
      axios.get(`http://${host}:${port}/departments/stats/gender`),
      axios.get(`http://${host}:${port}/employees/titles`),
      axios.get(`http://${host}:${port}/departments/stats/gender?type=manager`),
      axios.get(`http://${host}:${port}/departments/stats/salary`)
    ]).then(
      ([statsResponse, titlesResponse, managersResponse, salaryResponse]) => {
        this.setState({
          genderData: this.parseGenderData(statsResponse.data.data),
          titlesData: this.parseTitlesData(titlesResponse.data.data),
          managerData: this.parseManagerData(managersResponse.data.data),
          salaryData: this.parseSalaryData(salaryResponse.data.data),
          loading: false
        });
      }
    );
  }

  render() {
    const { classes } = this.props;
    return this.state.loading ? (
      <ProgressScreen />
    ) : (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={1} />
          <Grid item xs={10}>
            <Grid container spacing={3}>
              <Grid item xs={6}>
                <Paper className={classes.paper}>
                  <Doughnut
                    data={this.state.managerData}
                    options={{
                      cutoutPercentage: 50,
                      title: {
                        display: true,
                        text:
                          "Répartition des hommes et femmes dans les managers",
                        fontColor: "#9f9f9f",
                        fontSize: 18
                      },
                      legend: { labels: { fontColor: "#9f9f9f" } }
                    }}
                  />
                </Paper>
              </Grid>
              <Grid item xs={6}>
                <Paper className={classes.paper}>
                  <Radar
                    data={this.state.genderData}
                    options={{
                      title: {
                        display: true,
                        text:
                          "Répartition des hommes et femmes dans les départements",
                        fontColor: "#9f9f9f",
                        fontSize: 18
                      },
                      legend: { labels: { fontColor: "#9f9f9f" } },
                      scale: {
                        ticks: {
                          max: 100,
                          beginAtZero: true,
                          showLabelBackdrop: false,
                          fontColor: "#fff"
                        },
                        angleLines: { color: "#9f9f9f" },
                        gridLines: { color: "#9f9f9f" },
                        pointLabels: {
                          fontColor: "#9f9f9f"
                        }
                      }
                    }}
                  />
                </Paper>
              </Grid>
              <Grid item xs={6}>
                <Paper className={classes.paper}>
                  <Radar
                    data={this.state.salaryData}
                    options={{
                      title: {
                        display: true,
                        text: "Salaire moyen par département",
                        fontColor: "#9f9f9f",
                        fontSize: 18
                      },
                      legend: { labels: { fontColor: "#9f9f9f" } },
                      scale: {
                        ticks: {
                          showLabelBackdrop: false,
                          fontColor: "#fff"
                        },
                        angleLines: { color: "#9f9f9f" },
                        gridLines: { color: "#9f9f9f" },
                        pointLabels: {
                          fontColor: "#9f9f9f"
                        }
                      }
                    }}
                  />
                </Paper>
              </Grid>
              <Grid item xs={6}>
                <Paper className={classes.paper}>
                  <Bar
                    data={this.state.titlesData}
                    options={{
                      title: {
                        display: true,
                        text: "Nombre d'employés par poste",
                        fontColor: "#9f9f9f",
                        fontSize: 18
                      },
                      legend: { labels: { fontColor: "#9f9f9f" } }
                    }}
                  ></Bar>
                </Paper>
              </Grid>
            </Grid>
          </Grid>
          <Grid item xs={1} />
        </Grid>
      </div>
    );
  }
}

export default withStyles(styles)(Analyst);
