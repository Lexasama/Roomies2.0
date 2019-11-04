<template>
  <div id="app">
    <div id="centered_container">
      <h2 class="centered">Welcome</h2>

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
          <i class="fa fa-google" aria-hidden="true"></i>
          connectvia2
        </button>
        <br />
        <button
          id="centered_container"
          @click="login('Base')"
          class="btn btn-block btn-lg btn-dark"
        >connectvia2</button>
      </div>
    </div>
    <div style="padding-top: 3rem;"></div>
  </div>
</template>

<script>
import AuthService from "../services/AuthService";
import Vue from "vue";

export default {
  data() {
    return {
      endpoint: null
    };
  },

  mounted() {
    AuthService.registerAuthenticatedCallback(() => this.onAuthenticated());
  },

  beforeDestroy() {
    AuthService.removeAuthenticatedCallback(() => this.onAuthenticated());
  },

  methods: {
    login(provider) {
      AuthService.login(provider);
    },

    onAuthenticated() {
      this.$router.replace("/");
    }
  }
};
</script>

<style lang="scss">
iframe {
  width: 100%;
  height: 600px;
}
</style>