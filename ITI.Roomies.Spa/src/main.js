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

class currentColoc {
  constructor(colocId, colocName, picPath, date) {
    this.colocId = colocId;
    this.colocName = colocName;
    this.picPath = picPath;
    this.creationDate = date;
  }
  setColocId(newId) {
    this.colocId = newId;
  }

  setColocName(newName) {
    this.colocName = newName;
  }

  setPicPath(picPath) {
    this.picPath = picPath;
  }
  setDate(date) {
    this.creationDate = date;
  }
}
class user {
  constructor(
    userId,
    username,
    email,
    lastName,
    firstName,
    colocList,
    picPath
  ) {
    this.userId = userId;
    this.username = username;
    this.lastName = lastName;
    this.firstName = firstName;
    this.email = email;
    this.picPath = picPath;
    this.colocList = colocList;
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
  setLastName(name) {
    this.lastName = name;
  }
  setColocList(list) {
    this.colocList = list;
  }
  setPicPath(picPath) {
    this.picPath = picPath;
  }
}

Vue.use(VueGlobalVariable, {
  globals: {
    $currentColoc: new currentColoc(-1, "", "", ""),
    $user: new user()
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
