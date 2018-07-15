import React, { Component } from "react";
import { Route } from "react-router-dom";
import Landing from "../Landing/Landing";
import Men from "../Men/Men";
import About from "../About/About";
import Brands from "../Brands/Brands";
import Vlog from "../Vlog/Vlog";
import Login from "../Login/Login";
import Women from "../Women/Women";
import "./Main.css";

class Main extends Component {
  render() {
    return (
      <React.Fragment>
        <Route exact path="/" component={Landing} />
        <Route path="/men" component={Men} />
        <Route path="/about" component={About} />
        <Route path="/brands" component={Brands} />
        <Route path="/vlog" component={Vlog} />
        <Route path="/login" component={Login} />
        <Route path="/women" component={Women} />
      </React.Fragment>
    );
  }
}

export default Main;
