<template>
  <div>
    <div>
      <checkUser />
    </div>

    <div>
      <RadialMenu />
    </div>
  </div>
</template>

<script>
import { getUserAsync } from "@/api/UserApi";
import { getRoomieAsync } from "../api/RoomieApi";
import { getColocListAsync } from "../api/ColocApi";
import checkUser from "../components/Utility/CheckUser";
import AuthService from "../services/AuthService";
import RadialMenu from "./Utility/RadialMenu.vue";

export default {
  components: {
    checkUser,
    RadialMenu
  },
  data() {
    return {
      roomie: {}
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
            await this.setColocList();
            await this.setCurrentColoc();
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
    async setUser() {
      this.$user.setId(this.roomie.roomieId);
      this.$user.setEmail(AuthService.email);
      this.$user.setLastName(this.roomie.lastName);
      this.$user.setFirstName(this.roomie.firstName);
      this.$user.setUserName(this.roomie.userName);
      this.$user.setPicPath(this.roomie.picturePath);
      console.log("#Home: $user");
      console.log(this.$user);
    },
    async setCurrentColoc() {
      var coloc = this.$colocs.colocList[0];

      if (coloc != null) {
        this.$currentColoc.setCurrentColoc(coloc);
      }
      console.log("#Home: Current-coloc");
      console.log(this.$currentColoc);
    },
    async setColocList() {
      var colocs = await getColocListAsync(this.$user.userId);
      console.log("colocs", colocs);
      if (colocs.length > 0) {
        this.$colocs.setList(colocs);
      }
      console.log("#Home: currentColoc");
      console.log(this.$colocs);
    }
  }
};
</script>

<style lang="scss"></style>
