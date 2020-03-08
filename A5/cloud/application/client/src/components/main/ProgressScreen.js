import React from "react";
import CircularProgress from "@material-ui/core/CircularProgress";
import Grid from "@material-ui/core/Grid";

/**
 * Component showing "Fetching data" while client is fetching from API
 */
export default class ProgressScreen extends React.Component {
  render() {
    return (
      <Grid
        container
        spacing={0}
        direction="column"
        alignItems="center"
        justify="center"
        style={{ minHeight: "90vh" }}
      >
        <Grid item xs={3} style={{ textAlign: "center" }}>
          <h1>Fetching latest data...</h1>
          <CircularProgress size={80} color="secondary" />
        </Grid>
      </Grid>
    );
  }
}
