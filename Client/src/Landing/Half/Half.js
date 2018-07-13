import React, { Component } from "react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import "./Half.css";
@observer
class Half extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  render() {
    return <div className={`half ${this.props.type}`}>{this.props.type}</div>;
  }
}

export default Half;
