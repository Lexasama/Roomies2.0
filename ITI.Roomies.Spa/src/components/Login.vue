<template>
  <div>
    <div>
      <div class="container p-3">
        <div class="text-center">
          <h1 class="my-4">Bienvenue sur Roomies</h1>
          <b-card-group deck>
            <b-card header="Connexion" class="text-center">
              <button
                type="button"
                class="btn btn-lg btn-block btn-primary"
                @click="login('Google')"
              >
                <i class="fa fa-google" aria-hidden="true"></i> Se connecter via Google
              </button>
              <button
                type="button"
                class="btn btn-lg btn-block btn-primary"
                @click="login('Facebook')"
              >
                <i class="fa fa-facebook" aria-hidden="true"></i> Se connecter via Facebook
              </button>

              <button
                type="button"
                class="btn btn-lg btn-block btn-default"
                @click="login('Base')"
              >Se Connecter via Roomies</button>
            </b-card>
            <b-card header="Creer un compte"></b-card>
          </b-card-group>
        </div>
      </div>
    </div>
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