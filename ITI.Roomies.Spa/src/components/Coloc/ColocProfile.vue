<template>
  <div>
    <el-card>
      <b-row>
        <!-- LEFT COLUMN -->
        <b-col>colocList</b-col>

        <!-- Middle column  -->
        <b-col>
          <!-- PICTURE -->
          <b-row>
            <b-media>
              <template>
                <b-img :src="coloc.picPath" rounded width="180"></b-img>
              </template>
              <div>
                <b-button v-b-toggle.collapse-1 variant="primary">Changer de photo</b-button>
                <b-collapse id="collapse-1" class="mt-2">
                  <b-card>
                    <ImageUploader :id="this.colocId" isRoomie="false" />
                  </b-card>
                </b-collapse>
              </div>
            </b-media>
          </b-row>
          <el-divider></el-divider>

          <!-- ColocName Creationdate and Members -->

          <b-row>
            <div style="margin: 20px;"></div>
            <el-form label-position="right" label-width="100px" v-model="coloc" inline>
              <el-form-item label="Name">
                <el-input v-model="coloc.colocName"></el-input>
              </el-form-item>
              <el-button type="primary">Change Name</el-button>
              <el-form-item></el-form-item>

              <el-form-item label="Creation date">
                <el-input v-model="coloc.creationDate" disabled></el-input>
              </el-form-item>
            </el-form>
            <el-divider></el-divider>

            <div>
              <template>
                <el-table
                  ref="membresTable"
                  :data="members"
                  highlight-current-row
                  @current-change="handleCurrentChange"
                  style="width: 100%"
                >
                  <el-table-column property="LastName" label="Lastname"></el-table-column>
                  <el-table-column property="FirstName" label="Firstname"></el-table-column>
                  <el-table-column property="Phone" label="Phone"></el-table-column>
                </el-table>
              </template>
             </div>
          </b-row>
        </b-col>

        <!-- RIGTH COLUMN -->
        <b-col>roomie Profile</b-col>
      </b-row>

      <el-drawer
        title="I am the title"
        :visible.sync="drawer"
        :direction="direction"
        :before-close="handleClose"
      >
        <span>Hi, there!</span>
        <profile :roomie="roomie" />
      </el-drawer>
    </el-card>
  </div>
</template>

<script>
import ImageUploader from "../Utility/ImageUploader.vue";
import Profile from "../Roomie/Profile.vue";

export default {
  components: {
    ImageUploader,
    Profile
  },
  data() {
    return {
      roomie: {},
      profile: {},
      drawer: false,
      currentRow: null,
      colocId: 11,
      coloc: {
        colocName: "Fake Coloc",
        picPath:
          "data:image/png;base64,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",
        creationDate: "12 decembre 2017"
      },
      members: [
        {
          FirstName: "Axel",
          LastName: "SANON",
          Phone: "0123456789",
          BirthDate: new Date("January 31 1980"),
          Email: "testemail@gmail.com",
          Description:
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
          PicPath:
            "https://images.unsplash.com/photo-1518806118471-f28b20a1d79d?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&w=1000&q=80"
        },
        {
          FirstName: "Thomas",
          LastName: "Cousin",
          Phone: "0123456789",
          BirthDate: new Date("January 31 1980"),
          Email: "testemail@gmail.com",
          Description:
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
          PicPath:
            "https://images.unsplash.com/photo-1518806118471-f28b20a1d79d?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&w=1000&q=80"
        }
      ]
    };
  },

  async mounted() {},

  methods: {
    handleClose(done) {
      this.$confirm("Are you sure you want to close this?")
        .then(_ => {
          done();
        })
        .catch(_ => {});
    },

    handleCurrentChange(val) {
      this.currentRow = val;

      console.log(val);
      //console.log("Selected row" + row);

      this.drawer = true;
    },
    setCurrent(row) {
      this.$refs.membersTable.setCurrentRow(row);
    }
  }
}; //end export default
</script>
