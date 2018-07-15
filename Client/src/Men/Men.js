import React, { Component } from "react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import "./Men.css";

@observer
class Men extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  render() {
    return (
      <div className="men-wrap">
        <div className="content men">Men</div>
        <div className="women">Women</div>
      </div>
    );
  }
}

export default Men;
