<template>
  <div id="app">
    <div id="NavMenu">
      <header>
        <b-navbar toggleable="lg" type="dark" variant="info">
          <b-navbar-brand href="#"></b-navbar-brand>

          <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

          <b-collapse id="nav-collapse" is-nav>
            <b-navbar-nav>
              <b-nav-item href="#">Link</b-nav-item>
            </b-navbar-nav>

            <!-- Right aligned nav items -->
            <b-navbar-nav class="ml-auto">
              <b-nav-item-dropdown right>
                <!-- Using 'button-content' slot -->
                <template v-slot:button-content>
                  <em>User</em>
                </template>
                <b-dropdown-item href="#">Profile</b-dropdown-item>
                <b-dropdown-item href="#">Settings</b-dropdown-item>
                <b-dropdown-item href="/logout">Sign Out</b-dropdown-item>
              </b-nav-item-dropdown>
            </b-navbar-nav>
          </b-collapse>
        </b-navbar>
      </header>
    </div>

    <template>
      <div id="globalContainer">
        <main v-if="state == true " role="main">
          <loading />
        </main>
        <main v-else class="card containerCard">
          <router-view id="pageContent" class="child"></router-view>
        </main>
      </div>
    </template>
  </div>
</template>

<script>
import AuthService from "../services/AuthService";
import "../directives/requiredProviders";
import Loading from "../components/Utility/Loading.vue";
import Login from "../components/Login.vue";
import { state } from "../state";
import styles from "../styles/styles";

export default {
  components: {
    Loading,
    Login
  },

  data() {
    return {
      state,
      themeIdx: 0,
      state: true,
      styles: []
    };
  },

  async mounted() {
    this.state = true;
    this.styles = styles;
    console.log(this.styles);
    try {
      if (!AuthService.isConnected) {
        document.getElementById("NavMenu").style.display = "none";
      }
    } catch (e) {
      console.error(e);
    } finally {
      this.state = false;
    }

    if (this.$cookies.get("themeIdx") != undefined) {
      this.themeIdx = this.$cookies.get("themeIdx");
    } else {
      this.setTheme(0);
      this.themeIdx = this.$cookies.get(themeIdx);
    }
  },
  computed: {
    auth: () => AuthService,

    menuStyle() {
      return this.styles[this.themeIdx];
    },
    actualTheme() {
      return this.styles[this.themeIdx];
    },
    isLoading() {
      return this.state.isLoading;
    }
  },
  methods: {
    setTheme(themeIdx) {
      this.$cookies.set("themeIdx", themeIdx);
      this.themeIdx = themeIdx;
    }
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
<style lang="scss" scoped>
.progress {
  margin: 0px;
  padding: 0px;
  height: 5px;
}

a.router-link-active {
  font-weight: bold;
}
</style>

<style lang="scss">
@import "../styles/global.scss";
</style>

//  id="globalContainer"

//