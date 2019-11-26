<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="info">
      <b-navbar-brand href="/home">
        <img class="image" src="../../../public/favicon.png" />
      </b-navbar-brand>
      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <b-nav-item href="#">Link</b-nav-item>

          <b-nav-item @click="refresh()">
            <el-dropdown trigger="click" @command="handleCommand">
              <span class="el-dropdown-link">
                Coloc
                <i class="el-icon-arrow-down el-icon--right"></i>
              </span>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item v-for="c in colocList" :key="c.colocId">{{c.colocName}}</el-dropdown-item>
                <el-dropdown-item command="/coloc" divided>
                  Create
                  <i class="el-icon-circle-plus"></i>
                </el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
          </b-nav-item>
        </b-navbar-nav>

        <img src="../../../public/Logo.png" width="80" />
        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">
          <b-nav-item-dropdown right>
            <!-- Using 'button-content' slot -->
            <template v-slot:button-content>
              <em>{{ auth.email }}</em>
            </template>
            <b-dropdown-item href="/profile">Profile</b-dropdown-item>
            <b-dropdown-item href="/settings">Settings</b-dropdown-item>
            <b-dropdown-item href="/logout" @click="refreshApp()">Sign Out</b-dropdown-item>
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
  </div>
</template>

<script>
import AuthService from "@/services/AuthService";

export default {
  data() {
    return {
      colocList: []
    };
  },
  async mounted() {
    await this.refresh();
  },
  computed: {
    auth: () => AuthService
  },
  methods: {
    async refresh() {
      try {
        this.colocList = this.$user.colocList;
      } catch (error) {
        console.error(error);
      }
    },
    handleCommand(command) {
      this.$router.push("/coloc");
    }
  }
};
</script>

<style lang="scss">
.image {
  height: 50px !important;
  width: auto !important;
}
</style>