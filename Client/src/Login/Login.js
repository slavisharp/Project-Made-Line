import React, { Component } from "react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import './Login.css'

@observer
class Login extends Component {
  @observable a = "";

  // constructor(props) {
  //   super(props);
  // }

  render() {
    return <div className='content login'>Login</div>;
  }
}

export default Login;
