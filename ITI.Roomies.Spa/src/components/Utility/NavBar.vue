<template>
  <div>
    <div>
      <checkUser />
    </div>
    <b-navbar toggleable="lg" type="dark" variant="info">
      <b-navbar-brand href="/home">
        <el-image style="width: 80px; height: 80px" :src="colocPic" fit="fit">
          <div slot="placeholder" class="image-slot">
            Loading
            <span class="dot">...</span>
          </div>
        </el-image>
        <!-- <img src="../../../public/favicon.png" style="width: 60px; height: 60px" /> -->
      </b-navbar-brand>
      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <!-- <b-nav-item href="#">Link</b-nav-item> -->

          <b-nav-item @click="refresh()">
            <b-button variant="outline-primary" @click="drawer = true">Flats</b-button>
          </b-nav-item>
        </b-navbar-nav>

        <img class="image" src="../../../public/Logo.png" width="80" />
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
import { getPicAsync } from "@/api/RoomieApi.js";
import colocList from "../Coloc/ColocList";
import checkUser from "@/components/Utility/CheckUser";

export default {
  props: {
    navInfo: {
      type: Object,
      required: true
    }
  },
  components: {
    colocList,
    checkUser
  },

  data() {
    return {
      colocList: [],
      path: null,
      roomieId: null,
      drawer: false,
      colocPic: "http://localhost:5000/Pictures/ColocPics/default.png"
    };
  },
  async mounted() {
    // this.colocPic =
  },
  computed: {
    auth: () => AuthService
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
</style>