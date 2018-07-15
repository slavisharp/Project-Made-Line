import React, { Component } from "react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import Half from "./Half/Half";
import "./Landing.css";

@observer
class Landing extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  render() {
    return (
      <div className="landing">
        <Half type="men" />
        <Half type="women" />
      </div>
    );
  }
}

export default Landing;
