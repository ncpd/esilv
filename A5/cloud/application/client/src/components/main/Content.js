import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import { Route, Switch } from "react-router-dom";
import Grid from "@material-ui/core/Grid";
import User from './User'
import Analyst from "./Analyst";
import Administrators from "./Administrators";
import Error from "../errors/Error";

const useStyles = makeStyles(theme => ({
  root: {
    flexGrow: 1
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: "center",
    color: theme.palette.text.secondary
  }
}));

export default function CenteredGrid() {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <Grid container spacing={3}>
        <Switch>
          <Route exact path="/user">
            <User />
          </Route>
          <Route exact path="/analyst">
            <Analyst />
          </Route>
          <Route exact path="/admin">
            <Administrators />
          </Route>
          <Route>
            <Error code={404} message="Seems you're lost..." />
          </Route>
        </Switch>
      </Grid>
    </div>
  );
}
