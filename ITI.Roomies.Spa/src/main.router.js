// Vue router setup
import Vue from "vue";
import VueRouter from "vue-router";
Vue.use(VueRouter);

import requireAuth from "./helpers/requireAuth";

// Components
import Home from "./components/Home.vue";
import Login from "./components/Login.vue";
import Logout from "./components/Logout.vue";
import Register from "./components/Register.vue";
import ColocEdit from "./components/Coloc/ColocEdit.vue";
import UserChoise from "./components/UserChoise.vue";
import Profile from "./components/Roomie/Profile.vue";
import ColocProfile from "./components/Coloc/ColocProfile.vue";
import Test from "./components/Test.vue";

const routes = [
  { path: "", component: Home, beforeEnter: requireAuth },

  { path: "/login", component: Login },
  { path: "/logout", component: Logout, beforeEnter: requireAuth },
  { path: "/register", component: Register },
  { path: "/test", component: Test },
  { path: "/profile", component: Profile, beforeEnter: requireAuth },
  { path: "/coloc", component: ColocEdit, beforeEnter: requireAuth },
  { path: "/colocProfile", component: ColocProfile, beforeEnter: requireAuth }
];

export default new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});
