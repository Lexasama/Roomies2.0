import "./main.vendors";
import "./main.auth";
import AuthService from "./services/AuthService";
import Vue from "vue";
import App from "./components/App.vue";
import router from "./main.router";

import ElementUI from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
import axios from "axios";
import Vueaxios from "vue-axios";

import Bulma from "bulma";

import BootstrapVue from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import VueCookies from "vue-cookies";

Vue.use(BootstrapVue);
Vue.use(ElementUI);
Vue.use(VueCookies);

Vue.use(Vueaxios, axios);

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
