import React, { Component } from "react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import "./About.css";

@observer
class About extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  render() {
    return <div className="content about">About</div>;
  }
}

export default About;
