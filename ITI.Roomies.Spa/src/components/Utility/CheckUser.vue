<template></template>

<script>
import AuthService from "../../services/AuthService";
import { getColocListAsync } from "../../api/ColocApi";
import { getPicAsync } from "../../api/RoomieApi";
import { getUserAsync } from "../../api/UserApi";
export default {
  data() {
    return {
      roomie: {},
      navbarInfo: {}
    };
  }, //end data
  async mounted() {
    try {
      if (auth.isConnected) {
        console.log("you'reConnected");
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
  }, // end mounted
  computed: {
    auth: () => AuthService
  }, //end computed
  methods: {
    async setUser() {
      this.$user.setId(this.roomie.roomieId);
      this.$user.setEmail(this.roomie.email);
      this.$user.setLastName(this.roomie.lastName);
      this.$user.setFirstName(this.roomie.firstName);
      this.$user.setUserName(this.roomie.userName);
      this.$user.setPicPath(this.roomie.picturePath);

      console.log("#Home: $user");
      console.log(this.$user);
    },
    async setNavBar() {
      this.navbarInfo.email = this.AuthService.email;
      this.navbarInfo.picPath = getPicAsync();
      this.navbarInfo.colocList = this.$colocs.colocList;
      console.log("#Home: navbarInfo");
      console.log(this.navbarInfo);
    },
    async setColoc() {
      var colocs = getColocListAsync();

      this.$colocs.setList(colocs);

      if (this.$colocs.colocList.length < 0) {
        this.$colocs.currentColoc = this.$colocs.colocList[0];
      }
      console.log("#Home: currentColoc");
      console.log(this.$colocs);
    }
  } //end methods
}; // end
</script>