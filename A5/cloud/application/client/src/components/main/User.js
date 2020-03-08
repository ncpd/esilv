import React from "react";
import { withStyles } from "@material-ui/core/styles";
import { Paper, Grid, Button, Snackbar } from "@material-ui/core";
import axios from "axios";
import ProgressScreen from "../main/ProgressScreen";
import EnhancedTable from "./SortedTable";
import SalariesGraph from "./SalariesGraph";
import SearchBar from "./SearchBar";
import { config } from "../../config/default";
import SnackbarContent from "./SnackbarContent";

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
  secondary: {
    color: theme.palette.secondary.main
  },
  gradient: {
    background: theme.palette.secondary.mainGradient
  }
});

const headCells = [
  {
    id: "_id",
    numeric: true,
    disablePadding: false,
    label: "Employee n°"
  },
  {
    id: "first_name",
    numeric: false,
    disablePadding: false,
    label: "First Name"
  },
  { id: "last_name", numeric: false, disablePadding: false, label: "Last Name" }
];

class User extends React.Component {
  constructor() {
    super();
    this.handleSearchBarKeyPress = this.handleSearchBarKeyPress.bind(this);
    this.state = {
      loading: false,
      user: {
        emp_no: 10001, // init component with employee n°10001
        current_dept: {
          dept_name: "Development",
          dept_no: "d005" // init component with employee n°10001 and its dept
        }
      },
      manager: {},
      latestEmployees: [],
      salaries: {
        timeline: [],
        eachSalaries: []
      },
      toastStatus: false
    };
  }

  getAPIData() {
    this.setState({
      loading: true
    });
    const { user } = this.state;
    Promise.all([
      axios.get(
        `http://${host}:${port}/employees/` + user.emp_no + `/salaries/last`
      ),
      axios.get(
        `http://${host}:${port}/departments/` +
          user.current_dept.dept_no +
          `/managers`
      ),
      axios.get(
        `http://${host}:${port}/departments/` +
          user.current_dept.dept_no +
          `/employees/latest`
      ),
      axios.get(`http://${host}:${port}/employees/` + user.emp_no + `/salaries`)
    ]).then(
      ([
        salaryResponse,
        managersResponse,
        latestEmployeesResponse,
        salariesResponse
      ]) => {
        // Separate objets into two differents array for the graph
        const salariesObject = salariesResponse.data.data[0].salaries;
        let timeline = [],
          salaries = [];
        salariesObject.forEach(salary => {
          timeline.push(salary.from_date.substring(0, 4));
          salaries.push(salary.salary);
        });
        this.setState({
          user: salaryResponse.data.data[0],
          latestEmployees: latestEmployeesResponse.data.data,
          manager: managersResponse.data.data[0],
          salaries: { timeline, salaries },
          loading: false
        });
      }
    );
  }

  componentDidMount() {
    this.getAPIData();
  }

  handleRandomEmployeeClick() {
    this.setState({
      loading: true
    });
    axios
      .get(`http://${host}:${port}/employees/random`)
      .then(response => {
        return response.data;
      })
      .then(data => {
        this.setState(
          {
            user: data.data[0]
          },
          () => {
            this.getAPIData();
          }
        );
      });
  }

  setToastStatus(status) {
    this.setState({
      toastStatus: status
    });
  }

  handleSearchBarKeyPress(e) {
    if (e.key === "Enter") {
      if (typeof parseInt(e.target.value, 10) === "number") {
        axios
          .get(
            `http://${host}:${port}/employees/${e.target.value}/salaries/last`
          )
          .then(response => response.data)
          .then(data => {
            if (data.data && data.data.length === 1) {
              this.setState(
                {
                  user: data.data[0]
                },
                () => {
                  this.setState({
                    loading: true
                  });
                  this.setToastStatus(false);
                  this.getAPIData();
                }
              );
            } else {
              this.setToastStatus(true);
            }
          });
      }
    }
  }

  render() {
    const { classes } = this.props;
    const { user, salaries, loading, latestEmployees, manager } = this.state;

    return loading ? (
      <ProgressScreen />
    ) : (
      <div className={classes.root}>
        <Snackbar
          anchorOrigin={{
            vertical: "top",
            horizontal: "center"
          }}
          open={this.state.toastStatus}
          autoHideDuration={6000}
          onClose={() => this.setToastStatus(false)}
        >
          <SnackbarContent
            variant="error"
            message="No employee found"
            onClose={() => this.setToastStatus(false)}
          />
        </Snackbar>
        <Grid container spacing={3}>
          <Grid item xs={4}>
            <Grid container spacing={3}>
              <Grid item xs={12}>
                <SearchBar keyPressHandler={this.handleSearchBarKeyPress} />
              </Grid>
              <Grid item xs={6}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>EMPLOYEE N°</strong>
                  <h1>{user.emp_no}</h1>
                  <Button
                    className={classes.gradient}
                    variant="contained"
                    onClick={this.handleRandomEmployeeClick.bind(this)}
                  >
                    GET NEW
                  </Button>
                </Paper>
              </Grid>
              <Grid item xs={6}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>NAME</strong>
                  <h1>
                    {user.last_name} {user.first_name}
                  </h1>
                </Paper>
              </Grid>
              <Grid item xs={6}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>MANAGER</strong>
                  <h1>
                    {manager.firstName} {manager.lastName}
                  </h1>
                </Paper>
              </Grid>
              <Grid item xs={6}>
                <Paper className={classes.paper}>
                  <strong className={classes.secondary}>SALARY</strong>
                  <h1>${user.amount}</h1>
                </Paper>
              </Grid>
            </Grid>
          </Grid>
          <Grid item xs={4}>
            <Paper className={classes.paper}>
              <strong className={classes.secondary}>
                PROGRESSION OF SALARY
              </strong>
              <SalariesGraph salaries={salaries} />
            </Paper>
          </Grid>
          <Grid item xs={4}>
            <Paper className={classes.paper}>
              <strong className={classes.secondary}>
                LATEST EMPLOYEES IN{" "}
                {this.state.user.current_dept.dept_name.toUpperCase()}
              </strong>
              <EnhancedTable
                data={latestEmployees}
                headCells={headCells}
                title="Latest Employees"
              />
            </Paper>
          </Grid>
        </Grid>
      </div>
    );
  }
}

export default withStyles(styles)(User);
