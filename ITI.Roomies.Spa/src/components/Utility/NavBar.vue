<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="info">
      <b-navbar-brand>
        <router-link to="/ColocProfile">
          <el-image style="width: 80px; height: 80px" :src="getColocPic" fit="fit"></el-image>
        </router-link>
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
          <img
            class="image"
            src="https://media.discordapp.net/attachments/577862561534574634/672007130156367872/Logo.png?width=1434&height=677"
            width="80"
            alt="Picture"
          />
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
              <b-dropdown-item href="/logout">Sign Out</b-dropdown-item>
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
        <colocList @coloc-rejoin="refresh()" />
      </el-drawer>
    </b-navbar>
  </div>
</template>

<script>
import AuthService from "@/services/AuthService";
import { getPicAsync } from "../../api/RoomieApi.js";
import { getColocPicAsync } from "../../api/ColocApi.js";
import colocList from "../Coloc/ColocList";
import RadialMenu from "./RadialMenu.vue";
export default {
  components: {
    colocList,
    RadialMenu
  },

  data() {
    return {
      drawer: false,
      roomiePic: "http://localhost:5000/Default/user.png",
      colocPic: "http://localhost:5000/Default/favicon.png",
      roomieId: null,
      colocId: null
    };
  },
  async mounted() {
    this.roomieId = this.$user.roomieId;
    this.colocId = this.$currentColoc.colocId;

    await this.refresh();
  },
  computed: {
    auth: () => AuthService,
    getUserPic: function() {
      return this.roomiePic;
    },
    getColocPic: function() {
      return this.colocPic;
    }
  },
  methods: {
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
    },
    async refresh() {
      try {
        let p = await getPicAsync(this.roomieId);
        let c = await getColocPicAsync(this.colocId);
        this.colocPic = c.picPath;
        this.roomiePic = p.picturePath;
      } catch (error) {
        console.error(error);
      }

      this.$user.setPicPath(this.roomiePic);
      this.$currentColoc.setPicPath(this.colocPic);
    }
  } //end methods
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