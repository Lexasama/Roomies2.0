<template>
  <div>
    <el-card>
      <b-media>
        <template style="width: 100px; height: 100px;">
          <el-image
            style="width: 150px; height: 150px; border-radius: 50% !important; "
            :src="getColocPic"
            fit="scale-down"
          ></el-image>
        </template>

        <div>
          <b-button v-b-toggle.collapse-1 variant="primary">Change Picture</b-button>
          <b-collapse id="collapse-1" class="mt-2">
            <b-card>
              <ImageUploader :id="parseInt(colocId, 10)" :isRoomie="false" />
            </b-card>
          </b-collapse>
        </div>
      </b-media>

      <el-divider></el-divider>

      <!-- ColocName Creationdate and Members -->

      <div style="margin: 20px;"></div>
      <el-form label-position="right" label-width="100px" v-model="coloc" inline>
        <el-button @click="colocDawer = true">Flatsharings</el-button>

        <el-form-item label="Name">
          <el-input v-model="coloc.colocName"></el-input>
        </el-form-item>
        <el-button type="primary" @click="updateColoc">Change Name</el-button>
        <el-form-item></el-form-item>

        <el-form-item label="Creation date">
          <el-date-picker v-model="coloc.creationDate" disabled></el-date-picker>
        </el-form-item>
      </el-form>
      <el-divider></el-divider>

      <div>
        <el-button v-b-toggle.collapse-2 type="primary" icon="el-icon-plus">Invite</el-button>
        <b-collapse id="collapse-2" class="mt-2">
          <b-card>
            <invite />
          </b-card>
        </b-collapse>
        <el-divider></el-divider>

        <el-popover
          placement="bottom"
          title="Upload a new profile picture"
          trigger="click"
        >Invite form</el-popover>
      </div>
      <roomieList :colocId="getColocId" />

      <el-drawer title="Your list" :visible.sync="colocDawer" direction="ltr" size="50%">
        <div>
          <colocList />
        </div>
        <div></div>
      </el-drawer>
    </el-card>
  </div>
</template>

<script>
import ImageUploader from "../Utility/ImageUploader.vue";
import Profile from "../Roomie/RoomiesProfile.vue";
import invite from "../Roomie/Invite.vue";
import colocList from "../Coloc/ColocList.vue";
import ColocProfile from "../Coloc/ColocProfile";
import roomieList from "../Roomie/RoomieList";
import {
  getColocAsync,
  getColocListAsync,
  updateColocAsync
} from "../../api/ColocApi";
import { getRoomiesAsync } from "@/api/RoomieApi";

export default {
  components: {
    ImageUploader,
    Profile,
    ColocProfile,
    invite,
    colocList,
    roomieList
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
      selectedColoc: {}
    };
  },

  async mounted() {
    try {
      this.colocId = this.$route.params.colocId;
      console.log("params", this.$route.params.colocId);
      if (this.colocId == undefined) {
        //this.coloc = this.$currentColoc;
        //this.colocId = this.$currentColoc.colocId;
        console.log("curentcoloc Id", this.$currentColoc.colocId);
        if (this.$currentColoc.colocId == -1) {
          console.log("user Id", this.$user.userId);
          if (this.$user.userId == -1) {
            var colocs = getColocListAsync();
          }
          var colocs = getColocListAsync(this.$user.userId);
          if (colocs.length > 0) {
            this.$colocs.setList(colocs);
            this.$currentColoc.setCurrentColoc(colocs[0]);
          }
        }

        this.coloc = this.$currentColoc;
      } else {
        this.coloc = await getColocAsync(this.colocId);
      }
    } catch (error) {
      console.error(error);
    }
  },
  computed: {
    getColocPic: function() {
      return this.$currentColoc.picPath;
    },
    getColocId: function() {
      return this.colocId;
    }
  },
  methods: {
    show() {
      this.$message({
        showClose: true,
        message: "The name has been updated",
        type: "success"
      });
    },

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
    },
    async updateColoc() {
      let r = await updateColocAsync(this.coloc);
      this.show();
    }
  }
}; //end export default
</script>
