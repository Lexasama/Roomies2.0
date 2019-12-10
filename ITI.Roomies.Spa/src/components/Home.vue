<template>
  <div>
    <div>
      <checkUser />
    </div>
    <div>
      <radial-menu
        style="margin: auto; margin-top: 300px; background-color: white"
        :itemSize="50"
        :radius="130"
        :angle-restriction="360"
      >
        <radial-menu-item
          v-for="(item, index) in items"
          :key="index"
          style="background-color: white"
          @click="() => handleClick(item)"
        >
          <span>{{item}}</span>
        </radial-menu-item>
      </radial-menu>
      <div style="color: rgba(0,0,0,0.6); margin-top: 16px;">{{ lastClicked }}</div>
    </div>
  </div>
</template>

<script>
import { RadialMenu, RadialMenuItem } from "vue-radial-menu";
import { getUserAsync } from "@/api/UserApi";
import { getRoomieAsync } from "../api/RoomieApi";
import { getColocListAsync } from "../api/ColocApi";
import checkUser from "../components/Utility/CheckUser";
import AuthService from "../services/AuthService";

export default {
  components: {
    RadialMenu,
    RadialMenuItem,
    checkUser
  },
  data() {
    return {
      items: [
        "Taks",
        "Calendar",
        "Goceries",
        "Budget",
        "Settings",
        "more",
        "Roomie1",
        "Roomie2",
        "Roomie3",
        "Roomie4",
        "Roomie5"
      ],
      lastClicked: "click on something!",
      roomie: {},
      navbarInfo: {}
    };
  },
  async mounted() {
    try {
      if (this.auth.isConnected) {
        this.roomie = await getUserAsync();

        if (this.roomie.userName == null) {
          this.$router.replace("/register");
        } else {
          try {
            await this.setUser();
            await this.setColoc();
            await this.setNavBar();
          } catch (error) {
            console.error(error);
          }
        }
      }
    } catch (e) {
      console.error(e);
    }
  }, //end mounted

  computed: {
    auth: () => AuthService
  }, //end computed

  methods: {
    handleClick(item) {
      this.lastClicked = item;
    },

    async setUser() {
      // this.$user.userId = this.roomie.roomieId;
      this.$user.setId(this.roomie.roomieId);
      this.$user.setEmail(AuthService.email);
      this.$user.setLastName(this.roomie.lastName);
      this.$user.setFirstName(this.roomie.firstName);
      this.$user.setUserName(this.roomie.userName);
      this.$user.setPicPath(this.roomie.picturePath);
      let list = await getColocListAsync(this.$user.userId);
      this.$user.setColocList(list);
      console.log("#Home: $user");
      console.log(this.$user);
    },
    async setNavBar() {
      this.navbarInfo.email = this.$user.email;
      this.navbarInfo.picPath = this.$user.picPath;
      this.navbarInfo.colocList = this.$user.colocList;
      console.log("#Home: navbarInfo");
      console.log(this.navbarInfo);
    },
    async setColoc() {
      var colocData = this.$user.colocList[0];

      if (colocData != null) {
        this.$currentColoc.setColocId(colocData.colocId);
        this.$currentColoc.setColocName(colocData.colocName);
        this.$currentColoc.setPicPath(colocData.picPath);
        this.$currentColoc.setDate(colocData.creationDate);
      }
      console.log("#Home: currentColoc");
      console.log(this.$currentColoc);
    }
  }
};
</script>

<style lang="scss">
</style>