import React, { Component } from "react";
import { observable } from "mobx";
import { observer, inject } from "mobx-react";
import "./Vlog.css";

@inject(["stores"])
@observer
class Vlog extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  componentDidMount() {
    this.props.stores.CurrentPage.page = "vlog";
  }

  render() {
    return <div className="content vlog">Vlog</div>;
  }
}

export default Vlog;
