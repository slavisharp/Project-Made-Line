import React, { Component } from "react";
import "./Home.css";
import Header from "../Header/Header";
import Main from "../Main/Main";

class Home extends Component {
  render() {
    return (
      <div className='main'>
        <Header />
        <Main />
      </div>
    );
  }
}

export default Home;
