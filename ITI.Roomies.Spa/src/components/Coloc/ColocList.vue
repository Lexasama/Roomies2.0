<template>
  <div>
    <el-card>
      <div>
        <router-link to="/coloc">
          <el-button type="primary">Create</el-button>
        </router-link>
      </div>
      <div>
        <template v-on:coloccreated="this.show()">
          <el-table :data="colocList" style="width: 100%">
            <el-table-column prop="picPath" label width="110">
              <template slot-scope="scope">
                <el-image style="width:50px; height:50px" :src="scope.row.picPath" fit="scale-down"></el-image>
                <div class="block"></div>
              </template>
            </el-table-column>
            <el-table-column prop="colocName" label="Name" width="150"></el-table-column>

            <el-table-column label="Options" width="150">
              <template slot-scope="scope">
                <el-button-group>
                  <el-button
                    size="mini"
                    type="primary"
                    icon="el-icon-info"
                    @click="handleEdit(scope.$index, scope.row)"
                  ></el-button>
                  <el-button size="mini" @click="switchColoc(scope.row)">Switch</el-button>
                </el-button-group>
              </template>
            </el-table-column>
          </el-table>
        </template>

        <el-drawer title="Profile" :visible.sync="drawer" direction="rtl" :with-header="false">
          <ColocProfileRead :coloc="this.selectedColoc" />
        </el-drawer>
      </div>
    </el-card>
  </div>
</template>

<script>
import { EventBus } from "../Utility/event-bus";
import { getColocListAsync } from "../../api/ColocApi";
import ColocProfileRead from "./ColocProfileRead.vue";
export default {
  components: {
    ColocProfileRead
  },
  data() {
    return {
      colocList: [],
      drawer: false,
      selectedColoc: {}
    };
  },

  async mounted() {
    this.refreshList();
  }, //end mounted
  methods: {
    handleEdit(index, row) {
      console.log("index", index);
      console.log("row", row);
      this.selectedColoc = row;
      this.drawer = !this.drawer;
    },

    switchColoc(coloc) {
      console.log("colocToswitchTo", coloc);
      this.setColoc(coloc);
    },
    show() {
      this.$message({
        showClose: true,
        message: "event",
        type: "success"
      });
    },
    async refreshList() {
      this.colocList = await getColocListAsync();
      this.$colocs.setList(this.colocList);
    }
  } //end methods
};
</script>

<style lang="scss" scoped>
</style>