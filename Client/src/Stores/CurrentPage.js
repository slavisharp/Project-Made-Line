import { observable } from "mobx";

class CurrentPage {
  @observable page = "";
}

export default new CurrentPage();
