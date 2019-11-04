<template>
  <div id="app">
    <!-- Menu de navigation -->
    App
    <div id="app">
      <div id="centered_container">
        <h2 class="centered">Bienvenue</h2>

        <div>
          <img
            src="https://i.ibb.co/wc1n3HF/Roomies-logo-long.png"
            alt="Roomies-logo-long"
            border="0"
          />

          <button
            style="margin-top:16px"
            id="centered_container"
            @click="login('Google')"
            class="btn btn-lg btn-block btn-primary"
          >
            <i class="fa fa-google" aria-hidden="true"></i> Se connecter
          </button>
          <br />
          <button
            id="centered_container"
            @click="login('Base')"
            class="btn btn-block btn-lg btn-dark"
          >Se connecter</button>
        </div>
      </div>
      <div style="padding-top: 3rem;"></div>
    </div>
  </div>
</template>

<script>
import Login from "../components/Login";

export default {
  components: {
    Login
  }
};
</script>


<style lang="scss">
@import "../styles/global.scss";
</style>

<style lang="scss" scoped>
.el-menu-vertical-demo:not(.el-menu--collapse) {
  width: 12rem;
  max-width: 12rem;
  border-right: 1px solid #000000;
  float: left;
  margin-top: 0;
}
.el-menu-vertical-demo {
  float: left;
  min-height: 100vh;
  max-height: 100vh;
  border-right: 1px solid #000000;
  position: fixed;
  margin-top: 0;
}

.el-menu-item i,
.el-submenu i,
.el-menu-item.is-active,
.el-submenu-item.is-active {
  color: black;
}
.el-submenu-item,
.el-submenu-item.is-active {
  min-width: 12rem !important;
}
.el-button {
  background: rgb(102, 102, 102);
  border: 1px solid #000000;
  color: #000000;
}

#footer {
  bottom: 0;
  width: 100%;
  height: 8.5rem; /* Footer height */

  top: 2.5rem;
  padding-left: 12rem;
}

.form-control {
  padding: 0;
  margin-left: 0.5rem;
}
iframe {
  width: 50%;
  height: 600px;
}
#centered_container {
  margin-left: auto;
  margin-right: auto;
  max-width: 40em;
}
.centered {
  text-align: center;
  margin-top: 1.25rem;
  margin-bottom: 1.25rem;
}
.btn-block {
  max-width: 30em;
}
</style>
