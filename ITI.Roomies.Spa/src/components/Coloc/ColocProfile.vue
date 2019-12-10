<template>
  <div>
    <el-card>
      <b-media>
        <template>
          <b-img :src="coloc.picPath" rounded width="180"></b-img>
        </template>

        <div>
          <b-button v-b-toggle.collapse-1 variant="primary">Change Picture</b-button>
          <b-collapse id="collapse-1" class="mt-2">
            <b-card>
              <ImageUploader :id="this.colocId" :isRoomie="false" />
            </b-card>
          </b-collapse>
        </div>
      </b-media>

      <el-divider></el-divider>

      <!-- ColocName Creationdate and Members -->

      <div style="margin: 20px;"></div>
      <el-form label-position="right" label-width="100px" v-model="coloc" inline>
        <el-button @click="colocDawer = true">Flasharings</el-button>
        <el-form-item label="Name">
          <el-input v-model="coloc.colocName"></el-input>
        </el-form-item>
        <el-button type="primary">Change Name</el-button>
        <el-form-item></el-form-item>

        <el-form-item label="Creation date">
          <el-date-picker v-model="coloc.creationDate" disabled></el-date-picker>
        </el-form-item>
      </el-form>
      <el-divider></el-divider>

      <div>
        <!-- <b-button v-b-toggle.collapse-2 variant="primary">Invite</b-button>
        <b-collapse id="collapse-2" class="mt-2">
          <b-card></b-card>
        </b-collapse>-->

        <el-button v-b-toggle.collapse-2 type="primary" icon="el-icon-plus">Invite</el-button>
        <b-collapse id="collapse-2" class="mt-2">
          <b-card></b-card>
        </b-collapse>
        <el-divider></el-divider>

        <el-popover
          placement="bottom"
          title="Upload a new profile picture"
          trigger="click"
        >Invite form</el-popover>
        <template>
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

      <el-drawer title="Your list" :visible.sync="colocDawer" direction="ltr" size="50%">
        <div>
          <el-card>
            <template>
              <el-table
                ref="colocTable"
                :data="colocList"
                highlight-current-row
                @current-change="handleColocChange"
                style="width: 100%"
              >
                <el-table-column property="colocName" label="Name"></el-table-column>
                <el-table-column property="creationDate" label="Creation date"></el-table-column>
              </el-table>
            </template>
          </el-card>
        </div>
        <div>
          <!-- <el-drawer
            title="Profile"
            :append-to-body="true"
            :visible.sync="innerDrawer"
            direction="ltr"
          >
            <div>
              <ColocProfile :colocId="this.selectedColoc.colocId" />
            </div>
          </el-drawer>-->
        </div>
      </el-drawer>
    </el-card>
  </div>
</template>

<script>
import ImageUploader from "../Utility/ImageUploader.vue";
import Profile from "../Roomie/RoomiesProfile.vue";
import ColocProfile from "../Coloc/ColocProfile";
import { getColocAsync, getColocListAsync } from "../../api/ColocApi";
import { getRoomiesAsync } from "@/api/RoomieApi";

export default {
  components: {
    ImageUploader,
    Profile,
    ColocProfile
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
