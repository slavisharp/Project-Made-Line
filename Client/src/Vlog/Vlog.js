import React, { Component } from "react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import "./Vlog.css";

@observer
class Vlog extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  render() {
    return <div className="content vlog">Vlog</div>;
  }
}

export default Vlog;
