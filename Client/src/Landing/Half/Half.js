import React, { Component } from "react";
import { observable } from "mobx";
import { observer, inject } from "mobx-react";
import "./Half.css";

@inject(["stores"])
@observer
class Half extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  componentDidMount() {
    console.log(this.props.stores.CurrentPage.page);
  }

  render() {
    return <div className={`half ${this.props.type}`}>{this.props.type}</div>;
  }
}

export default Half;
