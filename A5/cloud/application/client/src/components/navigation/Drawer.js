import React from "react";
import { withStyles } from "@material-ui/core/styles";
import Drawer from "@material-ui/core/Drawer";
import CssBaseline from "@material-ui/core/CssBaseline";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import List from "@material-ui/core/List";
import Typography from "@material-ui/core/Typography";
import Divider from "@material-ui/core/Divider";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import GroupIcon from "@material-ui/icons/Group";
import EqualizerIcon from "@material-ui/icons/Equalizer";
import VerifiedUserIcon from "@material-ui/icons/VerifiedUser";
import CenteredGrid from "../main/Content";
import StyledLink from '../navigation/StyledLink'
import LinearProgress from "@material-ui/core/LinearProgress";

const drawerWidth = 240;

/**
 * 
 * @param {the inherited theme from the App component} theme 
 */
const styles = theme => ({
  root: {
    display: "flex"
  },
  appBar: {
    width: `calc(100% - ${drawerWidth}px)`,
    marginLeft: drawerWidth,
    backgroundColor: theme.palette.primary.light
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0
  },
  drawerPaper: {
    width: drawerWidth,
    backgroundColor: theme.palette.primary.light,
    color: "#fff"
  },
  toolbar: theme.mixins.toolbar,
  content: {
    flexGrow: 1,
    backgroundColor: theme.palette.primary.main,
    padding: theme.spacing(3),
    minHeight: "100vh"
  }
});

/**
 * Component representing left drawer in the admin dashboard
 */
class PermanentDrawerLeft extends React.Component {
  constructor() {
    super();
    this.state = {
      loading: false
    };
  }

  render() {
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <CssBaseline />
        <AppBar position="fixed" className={classes.appBar}>
          <Toolbar>
            <Typography variant="h6" noWrap>
              Company Dashboard
            </Typography>
          </Toolbar>
          {this.state.loading ? (
            <LinearProgress color="secondary" />
          ) : (
            <div></div>
          )}
        </AppBar>
        <Drawer
          className={classes.drawer}
          variant="permanent"
          classes={{
            paper: classes.drawerPaper
          }}
          anchor="left"
        >
          <div className={classes.toolbar} />
          <Divider />
          <List>
            <StyledLink to="/user">
              <ListItem button>
                <ListItemIcon>
                  <GroupIcon color="secondary" />
                </ListItemIcon>
                <ListItemText primary="Employees" />
              </ListItem>
            </StyledLink>
            <StyledLink to="/analyst">
              <ListItem button>
                <ListItemIcon>
                  <EqualizerIcon color="secondary" />
                </ListItemIcon>
                <ListItemText primary="Analysts" />
              </ListItem>
            </StyledLink>
            <StyledLink to="/admin">
              <ListItem button>
                <ListItemIcon>
                  <VerifiedUserIcon color="secondary" />
                </ListItemIcon>
                <ListItemText primary="Administrators" />
              </ListItem>
            </StyledLink>
          </List>
        </Drawer>
        <main className={classes.content}>
          <div className={classes.toolbar} />
          <CenteredGrid />
        </main>
      </div>
    );
  }
}

export default withStyles(styles)(PermanentDrawerLeft);
