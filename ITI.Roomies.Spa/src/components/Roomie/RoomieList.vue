<template>
  <div>
    <template>
      <el-table :data="memberList" style="width: 100%" @current-change="handleCurrentChange">
        <el-table-column prop="address">
          <template slot-scope="scope">
            <el-image
              style="width: 50px; height: 50px"
              :src="scope.row.picturePath"
              fit="scale-down"
            ></el-image>
          </template>
        </el-table-column>

        <el-table-column prop="firstName" label="First name"></el-table-column>
        <el-table-column prop="lastName" label="Last name"></el-table-column>
      </el-table>
    </template>

    <div>
      <el-drawer title="Profile" :visible.sync="roomieProfileDrawer" direction="rtl">
        <span></span>
        <RoomiesProfile :roomie="roomie" />
      </el-drawer>
    </div>
  </div>
</template>
<script>
import { getRoomiesAsync } from "../../api/RoomieApi.js";
import RoomiesProfile from "../Roomie/RoomiesProfile.vue";
export default {
  components: {
    RoomiesProfile
  },
  data() {
    return {
      memberList: [],
      roomieProfileDrawer: false,
      roomie: {}
    };
  },
  props: {
    colocId: {
      type: Number,
      required: false
    }
  },

  async mounted() {
    if (this.colocId == null) {
      this.colocId = this.$currentColoc.colocId;
    }
    console.log("#RoomieList colocId", this.colocId);
    this.refreshList();
  }, // end mounted
  methods: {
    async refreshList() {
      this.memberList = await getRoomiesAsync(this.colocId);
      console.log("memberList", this.memberList);
    },

    handleCurrentChange(val) {
      this.roomieProfileDrawer = !this.roomieProfileDrawer;
      this.roomie = val;
    }
  }
};
</script>
