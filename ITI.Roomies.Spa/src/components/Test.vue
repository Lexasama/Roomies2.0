<template>
  <div>
    <colocList />
  </div>
</template>

<script>
import profile from "../components/Coloc/ColocProfile.vue";
import register from "../components/Register.vue";
import roomieProfile from "../components/Roomie/RoomiesProfile";
import invite from "@//components/Roomie/Invite";
import colocProfile from "@/components/Coloc/ColocProfile";
import ImageUploader from "@/components/Utility/ImageUploader";
import colocList from "../components/Coloc/ColocList";
export default {
  components: {
    profile,
    register,
    roomieProfile,
    invite,
    colocProfile,
    ImageUploader,
    colocList
  },
  data() {
    return {
      tableData: [
        {
          date: "2016-05-03",
          name: "Tom",
          address: "No. 189, Grove St, Los Angeles"
        },
        {
          date: "2016-05-02",
          name: "Tom",
          address: "No. 189, Grove St, Los Angeles"
        },
        {
          date: "2016-05-04",
          name: "Tom",
          address: "No. 189, Grove St, Los Angeles"
        },
        {
          date: "2016-05-01",
          name: "Tom",
          address: "No. 189, Grove St, Los Angeles"
        }
      ],
      roomieId: 1,
      currentRow: null,
      roomie: {
        roomieId: 1,
        userName: "Lexa",
        lastName: "SANON",
        firstName: "Axel",
        email: "saxelsyeguillaume@gmail.com",
        phone: "0123456456",
        sex: 1,
        birthDate: "1999-01-01T00:00:00",
        description: "i love Roomies",
        picturePath: "http://localhost:5000/Pictures/RoomiesPics/1.jpeg"
      }
    };
  },
  methods: {
    setCurrent(row) {
      this.$refs.singleTable.setCurrentRow(row);
    },
    handleCurrentChange(val) {
      this.currentRow = val;
    }
  }
};
</script>