import React, { Component } from "react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import "./Brands.css";

@observer
class Brands extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  render() {
    return <div className="content brands">Brands</div>;
  }
}

export default Brands;
