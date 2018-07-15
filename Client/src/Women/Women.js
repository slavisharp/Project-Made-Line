import React, { Component } from "react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import "./Women.css";

@observer
class Women extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  render() {
    return (
      <div className="women-wrap">
        <div className="men">Men</div>
        <div className="content women">Women</div>;
      </div>
    );
  }
}

export default Women;
