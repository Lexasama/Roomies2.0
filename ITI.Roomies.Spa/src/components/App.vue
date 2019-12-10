<template>
  <div id="app">
    <div id="NavMenu" v-if="this.auth.isConnected">
      <!-- HEADER WITH NAVBAR -->
      <header>
        <navBar :navInfo="navbarInfo" />
      </header>
    </div>

    <template>
      <div id="globalContainer">
        <main v-if="state == true" role="main">
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
import UserService from "../services/UserService";
import "../directives/requiredProviders";
import Loading from "../components/Utility/Loading.vue";
import Login from "../components/Login.vue";
import { state } from "../state";
import styles from "../styles/styles";
import NavBar from "../components/Utility/NavBar";
import { getUserAsync } from "../api/UserApi";

export default {
  components: {
    Loading,
    Login,
    NavBar
  },

  data() {
    return {
      state,
      themeIdx: null,
      state: true,
      styles: [],
      roomie: {},
      navbarInfo: {}
    };
  },
  async mounted() {
    this.state = true;
    this.styles = styles;
    try {
      // this.roomie = await getUserAsync();
      // if (this.roomie.userName != null) {
      //   await this.setUser();
      // }
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
    menuStyle() {
      return this.styles[this.themeIdx];
    },
    actualTheme() {
      return this.styles[this.themeIdx];
    },
    isLoading() {
      return this.state.isLoading;
    },
    auth: () => AuthService,
    user: () => UserService
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

.el-icon-arrow-down {
  font-size: 12px;
}
</style>

<style lang="scss">
@import "../styles/global.scss";
</style>
