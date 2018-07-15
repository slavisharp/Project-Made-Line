import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import "./reset.css";
import Home from "./Home/Home";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "mobx-react";
import stores from "./Stores/index";

ReactDOM.render(
  <BrowserRouter>
    <Provider stores={stores}>
      <Home />
    </Provider>
  </BrowserRouter>,
  document.getElementById("root")
);
