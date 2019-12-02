<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="info">
      <b-navbar-brand href="/home">
        <img src="../../../public/favicon.png" style="width: 60px; height: 60px" />
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
                <el-dropdown-item
                  :command="c.colocId"
                  v-for="c in colocList"
                  :key="c.colocId"
                >{{c.colocName}}</el-dropdown-item>
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
            <el-avatar :size="50" :src="path"></el-avatar>
          </b-navbar-brand>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script>
import AuthService from "@/services/AuthService";
import { getPicAsync } from "@/api/RoomieApi.js";

export default {
  props: {
    navInfo: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      colocList: [],
      path: null,
      roomieId: null
    };
  },
  async mounted() {
    this.roomieId;
    //this.path = await getPicAsync(this.roomieId);
    console.log("this.navInfo");
    console.log(this.navInfo);
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
      if (command == "/coloc") {
        this.$router.push("/coloc");
      } else {
        this.$router.push(`/colocProfile/${command}`);
      }
    },
    errorHandler() {
      return true;
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