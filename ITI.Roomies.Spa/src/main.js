import "./main.vendors";
import "./main.auth";
import AuthService from "./services/AuthService";
import Vue from "vue";
import App from "./components/App.vue";
import router from "./main.router";

import ElementUI from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
import locale from "element-ui/lib/locale/lang/fr";
import axios from "axios";
import Vueaxios from "vue-axios";
import VueGlobalVariable from "vue-global-var";

import BootstrapVue from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import VueCookies from "vue-cookies";

Vue.use(BootstrapVue);
Vue.use(ElementUI, { locale });
Vue.use(VueCookies);
Vue.use(Vueaxios, axios);

class styles {
  contructor(name, style, appStyle) {
    this.name = name;
    this.style = style;
    this.appStyle = appStyle;
  }
}
class currentColoc {
  constructor(colocId, colocName, creationDate, picPath) {
    this.colocId = colocId;
    this.colocName = colocName;
    this.creationDate = creationDate;
    this.picPath = picPath;
  }

  setCurrentColoc(coloc) {
    this.colocId = coloc.colocId;
    this.colocName = coloc.colocName;
    this.creationDate = coloc.creationDate;
    if (coloc.picPath != null) {
      this.picPath = coloc.picPath;
    }
  }
  setColocId(colocId) {
    this.colocId = colocId;
  }

  setColocName(colocName) {
    this.colocName = colocName;
  }

  setCreationDate(date) {
    this.creationDate = date;
  }

  setPicPath(picPath) {
    this.picPath = picPath;
  }
}
class colocs {
  constructor(list) {
    this.colocList = list;
  }
  setList(list) {
    this.colocList = list;
  }
}
class user {
  constructor(userId, userName, email, lastName, firstName, picPath) {
    this.userId = userId;
    this.userName = userName;
    this.lastName = lastName;
    this.firstName = firstName;
    this.email = email;
    this.picPath = picPath;
  }
  setId(id) {
    this.userId = id;
  }
  setEmail(email) {
    this.email = email;
  }
  setFirstName(name) {
    this.firstName = name;
  }
  setUserName(userName) {
    this.userName = userName;
  }
  setLastName(name) {
    this.lastName = name;
  }
  setPicPath(picPath) {
    this.picPath = picPath;
  }
  getPicPath() {
    return this.picPath;
  }
}

Vue.use(VueGlobalVariable, {
  globals: {
    $colocs: new colocs(null),
    $currentColoc: new currentColoc(
      -1,
      "",
      new Date(),
      "http://localhost:5000/Pictures/ColocPics/default.png"
    ),
    $user: new user(
      -1,
      "",
      "",
      "",
      "",
      "http://localhost:5000/Pictures/RoomiesPics/default.png"
    ),
    $styles: new styles()
  }
});

Vue.config.productionTip = false;

const main = async () => {
  await AuthService.init();

  new Vue({
    router,
    el: "#app",
    render: h => h(App)
  }).$mount("#app");
};

main();
