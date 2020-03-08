import React from "react";
import Drawer from "./components/navigation/Drawer";
import { BrowserRouter as Router } from "react-router-dom";

function App() {
  return (
    <Router>
      <div>
        <Drawer />
      </div>
    </Router>
  );
}

export default App;
