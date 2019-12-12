<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="info">
      <b-navbar-brand>
        <router-link to="/ColocProfile">
          <el-image style="width: 80px; height: 80px" :src="getColocPic" fit="fit">
            <div slot="placeholder" class="image-slot">
              Loading
              <span class="dot">...</span>
            </div>
          </el-image>
        </router-link>
        <!-- <img src="../../../public/favicon.png" style="width: 60px; height: 60px" /> -->
      </b-navbar-brand>
      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <!-- <b-nav-item href="#">Link</b-nav-item> -->

          <b-nav-item @click="refresh()">
            <b-button variant="outline-primary" @click="drawerSwitch()">Flats</b-button>
          </b-nav-item>
        </b-navbar-nav>
        <router-link to="/home">
          <img class="image" src="../../../public/Logo.png" width="80" />
        </router-link>
        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">
          <b-nav-item-dropdown right>
            <!-- Using 'button-content' slot -->
            <template v-slot:button-content>
              <em>{{ auth.email }}</em>
            </template>
            <router-link to="/profile">
              <b-dropdown-item href="/profile">Profile</b-dropdown-item>
            </router-link>
            <router-link to="/settings">
              <b-dropdown-item href="/settings">Settings</b-dropdown-item>
            </router-link>
            <router-link to="/logout">
              <b-dropdown-item href="/logout" @click="refreshApp()">Sign Out</b-dropdown-item>
            </router-link>
          </b-nav-item-dropdown>
        </b-navbar-nav>
        <b-navbar-nav>
          <b-navbar-brand>
            <div style>
              <router-link to="/profile">
                <el-image class="avatar" :src="getUserPic" fit="scale-down"></el-image>
              </router-link>
            </div>
          </b-navbar-brand>
        </b-navbar-nav>
      </b-collapse>

      <el-drawer
        title="List of your flats"
        :visible.sync="drawer"
        direction="ltr"
        :with-header="false"
      >
        <colocList />
      </el-drawer>
    </b-navbar>
  </div>
</template>

<script>
import AuthService from "@/services/AuthService";
import { getPicAsync } from "../../api/RoomieApi.js";
import colocList from "../Coloc/ColocList";

export default {
  components: {
    colocList
  },

  data() {
    return {
      colocList: [],
      drawer: false
    };
  },
  async mounted() {},
  computed: {
    auth: () => AuthService,
    getUserPic: function() {
      return this.$user.picPath;
    },
    getColocPic: function() {
      return this.$currentColoc.picPath;
    }
  },
  methods: {
    async refresh() {
      try {
        this.colocList = this.$colocs.colocList;
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
    },
    drawerSwitch() {
      this.drawer = !this.drawer;
    }
  }
};
</script>

<style lang="scss">
.image {
  position: absolute;
  left: 50%;
  margin: 0 auto;
  width: 80;
}
.avatar {
  width: 80px;
  height: 80px;
  border-radius: 50% !important;
}
</style>