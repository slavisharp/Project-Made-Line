import React, { Component } from "react";
import { observable } from "mobx";
import { observer, inject } from "mobx-react";
import "./Women.css";

@inject(["stores"])
@observer
class Women extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  componentDidMount() {
    this.props.stores.CurrentPage.page = "women";
  }

  render() {
    return (
      <div className="women-wrap">
        <div className="men content-padded">Men</div>
        <div className="content women content-padded">Women</div>
      </div>
    );
  }
}

export default Women;
