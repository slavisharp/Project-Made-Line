import React, { Component } from "react";
import { observable } from "mobx";
import { observer, inject } from "mobx-react";
import "./Half.css";

@inject(["stores"])
@observer
class Half extends Component {
  @observable comingFromPage = "";

  constructor(props) {
    super(props);
    this.handleComingFromPage();
  }

  handleComingFromPage = () => {
    if (this.props.stores.CurrentPage.page == "men") {
      this.comingFromPage = "coming-from-" + this.props.stores.CurrentPage.page;
    } else if (this.props.stores.CurrentPage.page == "women") {
      this.comingFromPage = "coming-from-" + this.props.stores.CurrentPage.page;
    } else {
      return;
    }
  };

  componentWillUnmount() {
    this.props.stores.CurrentPage.page = "";
  }

  render() {
    return (
      <div className={`half ${this.props.type} ${this.comingFromPage}`}>
        {this.props.type}
      </div>
    );
  }
}

export default Half;
