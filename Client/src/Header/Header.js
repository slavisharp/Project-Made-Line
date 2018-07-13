import React, { Component } from "react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import { Link } from "react-router-dom";
import "./Header.css";

@observer
class Header extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  render() {
    return (
      <header className="header">
        <nav>
          <Link to="/men" className="special pull-left nav-men">
            Men
          </Link>
          <Link to="/about" className="nav-men">
            About
          </Link>
          <Link to="/brands" className="nav-men">
            Brands
          </Link>
          <Link to="/">
            <img src="favicon.ico" alt="Logo" />
          </Link>
          <Link to="/vlog" className="nav-women">
            Vlog
          </Link>
          <Link to="/login" className="nav-women">
            Login
          </Link>
          <Link to="/women" className="special pull-right nav-women">
            Women
          </Link>
        </nav>
      </header>
    );
  }
}

export default Header;
