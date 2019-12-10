<template>
  <div>
    <el-card>
      <div>
        <router-link to="/coloc">
          <el-button type="primary">Create</el-button>
        </router-link>
      </div>
      <div>
        <template>
          <el-table :data="colocList" style="width: 100%">
            <el-table-column prop="picPath" label width="110">
              <template slot-scope="scope">
                <el-avatar shape="square" :size="50" :src="scope.row.picPath"></el-avatar>
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
          <ColocProfileRead :colocId="this.selectedColoc.colocId" />
        </el-drawer>
      </div>
    </el-card>
  </div>
</template>

<script>
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
    this.colocList = await getColocListAsync();
    console.log(this.$user.userId);
    console.log(this.colocList);
  }, //end mounted
  methods: {
    handleEdit(index, row) {
      console.log(index);
      console.log(row);
      this.selectedColoc = row;
      this.drawer = !this.drawer;
    },

    switchColoc(coloc) {
      console.log(coloc);
      this.setColoc(coloc);
    },
    async setColoc(coloc) {
      var colocData = coloc;

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

<style lang="scss" scoped>
</style>