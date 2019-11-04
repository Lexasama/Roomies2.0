// Vue router setup
import Vue from "vue";
import VueRouter from "vue-router";
Vue.use(VueRouter);

// Components
import Home from "./components/Home.vue";
import Login from "./components/Login.vue";
import Logout from "./components/Logout.vue";

const routes = [
  { path: "/", component: Home },

  { path: "/login", component: Login },
  { path: "/logout", component: Logout }
];

export default new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});
