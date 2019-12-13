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
import { get } from "http";

export default {
  components: {
    RadialMenu,
    RadialMenuItem,
    checkUser
  },
  data() {
    return {
      items: ["Tasks", "Calendar", "Groceries", "Settings", "more", "Axel"],
      lastClicked: "click on something!",
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
    handleClick(item) {
      console.log("item", item);
      this.lastClicked = item;

      if (item == "Tasks") {
        this.$router.push("/Tasks");
      }
    },

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

<style lang="scss">
</style>