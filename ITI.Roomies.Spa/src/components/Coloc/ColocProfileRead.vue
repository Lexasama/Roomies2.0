<template>
  <div>
    <el-card>
      <div class="block">
        <el-avatar shape="square" :size="60" :src="coloc.picPath"></el-avatar>
      </div>

      <el-divider></el-divider>

      <!-- ColocName Creationdate and Members -->

      <div style="margin: 20px;"></div>
      <el-form label-position="right" label-width="100px" v-model="coloc" inline>
        <el-form-item label="Name">
          <el-input v-model="coloc.colocName" disabled></el-input>
        </el-form-item>
        <el-form-item></el-form-item>

        <el-form-item label="Creation date">
          <el-date-picker v-model="coloc.creationDate" disabled></el-date-picker>
        </el-form-item>
      </el-form>
      <el-divider></el-divider>

      <div>
        <template>
          <h3>List of Roomies</h3>
          <el-divider></el-divider>
          <el-table
            ref="membresTable"
            :data="members"
            highlight-current-row
            @current-change="handleCurrentChange"
            style="width: 100%"
          >
            <el-table-column property="lastName" label="Lastname"></el-table-column>
            <el-table-column property="firstName" label="Firstname"></el-table-column>
            <el-table-column property="phone" label="Phone"></el-table-column>
          </el-table>
        </template>
      </div>

      <el-drawer title="Profile" :visible.sync="drawer1" direction="rtl">
        <span></span>
        <profile :roomie="roomie" />
      </el-drawer>
    </el-card>
  </div>
</template>

<script>
import Profile from "../Roomie/RoomiesProfile.vue";
import { getColocAsync } from "../../api/ColocApi";
import { getRoomiesAsync } from "@/api/RoomieApi";

export default {
  components: {
    Profile
  },
  props: {
    colocId: {
      type: Number,
      required: false
    }
  }, //end props
  data() {
    return {
      roomie: {},
      profile: {},
      drawer1: false,
      colocDawer: false,
      innerDrawer: false,
      currentRow: null,
      coloc: {},
      members: [],
      colocList: [],
      selectedColoc: {}
    };
  },

  async mounted() {
    try {
      this.colocId = this.$route.params.colocId;

      if (this.colocId == null) {
        this.coloc = this.$currentColoc;
        this.colocId = this.$currentColoc.colocId;
      } else {
        this.coloc = await getColocAsync(this.colocId);
      }

      this.members = await getRoomiesAsync(this.colocId);
      this.colocList = await getColocListAsync(this.$user.userId);
    } catch (error) {
      console.error(error);
    }
  },
  computed: {},
  methods: {
    handleClose(done) {
      this.$confirm("You still have unsaved data, proceed?")
        .then(_ => {
          done();
        })
        .catch(_ => {});
    },
    handleColocChange(coloc) {
      this.innerDrawer = true;
      this.selectedColoc = coloc;
      console.log(coloc.colocId);
    },

    handleCurrentChange(val) {
      this.currentRow = val;

      console.log(val);
      //console.log("Selected row" + row);
      this.roomie = val;
      this.drawer1 = !this.drawer1;
    },
    setCurrent(row) {
      this.$refs.membersTable.setCurrentRow(row);
    }
  }
}; //end export default
</script>
