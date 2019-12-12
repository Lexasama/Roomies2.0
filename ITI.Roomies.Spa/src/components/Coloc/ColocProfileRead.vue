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
          <roomieList :colocId="coloc.colocId" />
        </template>
      </div>
    </el-card>
  </div>
</template>

<script>
import Profile from "../Roomie/RoomiesProfile.vue";
import colocList from "../Coloc/ColocList.vue";
import { getColocAsync } from "../../api/ColocApi";
import { getRoomiesAsync } from "@/api/RoomieApi";
import roomieList from "../Roomie/RoomieList";

export default {
  components: {
    Profile,
    colocList,
    roomieList
  },
  props: {
    coloc: {
      type: Object,
      required: true
    }
  }, //end props
  data() {
    return {
      coloc: {}
    };
  },

  async mounted() {
    console.log("coloc", this.coloc);
  },
  computed: {
    getColoc: function() {
      return this.coloc;
    }
  }, //end computed
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
