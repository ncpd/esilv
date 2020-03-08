import React from "react";
import { withStyles } from "@material-ui/core/styles";
import Grid from "@material-ui/core/Grid";
import { Typography, Button } from "@material-ui/core";
import StyledLink from "../navigation/StyledLink";

/**
 *
 * @param {Inherited theme from the App} theme
 */
const styles = theme => ({
  root: {
    flexGrow: 1
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: "center",
    color: theme.palette.text.primary
  }
});

/**
 * Component in charge of redirecting user to /user if he got a 404
 */
class Error extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      code: this.props.code ? this.props.code : 500,
      message: this.props.message
        ? this.props.message
        : "You can go home by visiting /user"
    };
  }

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
        <Grid item xs={5} style={{ textAlign: "center", fontSize: "30px" }}>
          <h1 style={{ marginBottom: 0 }}>{this.state.code}</h1>
          <h3 style={{ marginTop: 0, marginBottom: 0 }}>
            Something wrong happened !
          </h3>
          <Typography style={{ marginTop: 0 }}>{this.state.message}</Typography>
          <StyledLink to="/user">
            <Button variant="contained" color="secondary">
              go back
            </Button>
          </StyledLink>
        </Grid>
      </Grid>
    );
  }
}

export default withStyles(styles)(Error);
