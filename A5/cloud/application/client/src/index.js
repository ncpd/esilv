import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import { MuiThemeProvider, createMuiTheme } from "@material-ui/core/styles";

/**
 * Material UI theme creation
 */
const theme = createMuiTheme({
  palette: {
    primary: {
      light: "#333",
      main: "#212121",
      dark: "#000"
    },
    secondary: {
      main: "#f59418",
      mainGradient: "linear-gradient(180deg, rgba(240,90,40,1), rgba(250,199,11,1))"
    },
    text: {
      primary: "#9f9f9f"
    }
  },
  typography: {
    useNextVariants: true
  }
});

ReactDOM.render(
  <MuiThemeProvider theme={theme}>
    <App />
  </MuiThemeProvider>,
  document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
