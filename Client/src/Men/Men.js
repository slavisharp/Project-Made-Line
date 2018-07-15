import React, { Component } from "react";
import { observable } from "mobx";
import { observer, inject } from "mobx-react";
import "./Men.css";

@inject(["stores"])
@observer
class Men extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  componentDidMount() {
    this.props.stores.CurrentPage.page = "men";
  }

  render() {
    return (
      <div className="men-wrap">
        <div className="content men content-padded">Men</div>
        <div className="women content-padded">Women</div>
      </div>
    );
  }
}

export default Men;
