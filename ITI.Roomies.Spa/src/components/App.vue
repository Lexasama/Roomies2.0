<template>
  <div id="app">
    <div id="NavMenu">
      <header>
        <b-navbar toggleable="lg" type="dark" variant="info">
          <b-navbar-brand href="/home">
            <img class="image" src="../../public/favicon.png" />
          </b-navbar-brand>
          <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

          <b-collapse id="nav-collapse" is-nav>
            <b-navbar-nav>
              <b-nav-item href="#">Link</b-nav-item>
              <!-- <b-nav-item>
                <div>
                  <b-button v-b-toggle.collapse-1 variant="outline-*">Coloc</b-button>
                  <b-collapse id="collapse-1" class="mt-2">
                   
                    <b-card>create coloc</b-card>
                  </b-collapse>
                </div>
              </b-nav-item>-->

              <b-nav-item>
                <el-dropdown>
                  <span class="el-dropdown-link">
                    Coloc
                    <i class="el-icon-arrow-down el-icon--right"></i>
                  </span>
                  <el-dropdown-menu slot="dropdown">
                    <el-dropdown-item v-for="c in colocList" :key="c">{{c}}</el-dropdown-item>
                    <el-dropdown-item divided>
                      Create
                      <i class="el-icon-circle-plus"></i>
                    </el-dropdown-item>
                  </el-dropdown-menu>
                </el-dropdown>
              </b-nav-item>
            </b-navbar-nav>

            <img src="../../public/Logo.png" width="80" />
            <!-- Right aligned nav items -->
            <b-navbar-nav class="ml-auto">
              <b-nav-item-dropdown right>
                <!-- Using 'button-content' slot -->
                <template v-slot:button-content>
                  <em>{{auth.email}}</em>
                </template>
                <b-dropdown-item href="/profile">Profile</b-dropdown-item>
                <b-dropdown-item href="/settings">Settings</b-dropdown-item>
                <b-dropdown-item href="/logout">Sign Out</b-dropdown-item>
              </b-nav-item-dropdown>
            </b-navbar-nav>
            <b-navbar-nav>
              <b-navbar-brand href="/profile">
                <b-img
                  src="https://banner2.cleanpng.com/20180319/rlq/kisspng-computer-icons-user-profile-avatar-profile-transparent-png-5ab03f3def8981.4074689915214999659812.jpg"
                  width="80"
                  rounded="circle"
                ></b-img>
              </b-navbar-brand>
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
      styles: [],
      colocList: ["coloc1", "coloc2", "coloc3"]
    };
  },

  async mounted() {
    this.state = true;
    this.styles = styles;
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
.image {
  height: 50px !important;
  width: auto !important;
}

.el-icon-arrow-down {
  font-size: 12px;
}
</style>

<style lang="scss">
@import "../styles/global.scss";
</style>