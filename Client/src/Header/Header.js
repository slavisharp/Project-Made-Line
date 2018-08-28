import React, { Component } from "react";
import { observable } from "mobx";
import { observer, inject } from "mobx-react";
import { Link } from "react-router-dom";
import "./Header.css";

@inject(["stores"])
@observer
class Header extends Component {
  @observable color = "";

  handleColor = type => {
    if (type === "men" || window.location.pathname === "/men") {
      this.color = "light";
    } else if (type === "women" || window.location.pathname === "/women") {
      this.color = "dark";
    } else {
      return;
    }
  };

  render() {
    return (
      <header className="header">
        <nav className="header-padded">
          <Link
            to="/men"
            onClick={() => this.handleColor("men")}
            className="special pull-left nav-men"
          >
            Men
          </Link>
          <Link to="/about" className={`nav-men ${this.color}`}>
            About
          </Link>
          <Link to="/brands" className={`nav-men ${this.color}`}>
            Brands
          </Link>
          <Link to="/">
            <img src="favicon.ico" alt="Logo" />
          </Link>
          <Link to="/vlog" className={`nav-women ${this.color}`}>
            Vlog
          </Link>
          <Link to="/login" className={`nav-women ${this.color}`}>
            Login
          </Link>
          <Link
            to="/women"
            onClick={() => this.handleColor("women")}
            className="special pull-right nav-women"
          >
            Women
          </Link>
        </nav>
      </header>
    );
  }
}

export default Header;
