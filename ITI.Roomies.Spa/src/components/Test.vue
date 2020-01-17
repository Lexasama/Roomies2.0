<template>
  <div>
    <theme />
  </div>
</template>

<script>
import profile from "../components/Coloc/ColocProfile.vue";
import register from "../components/Register.vue";
import invite from "@//components/Roomie/Invite";
import colocProfile from "@/components/Coloc/ColocProfile";
import ImageUploader from "@/components/Utility/ImageUploader";
import colocList from "../components/Coloc/ColocList";
import roomieList from "../components/Roomie/RoomieList";
import TasksCreate from "../components/Tasks/TaskCreate";
import taskHistory from "./Tasks/TaskHistory.vue";
import taskUpdate from "./Tasks/TaskUpdate.vue";
import theme from "./Settings/Theme.vue";

export default {
  components: {
    profile,
    register,
    invite,
    colocProfile,
    ImageUploader,
    colocList,
    roomieList,
    TasksCreate,
    taskHistory,
    taskUpdate,
    theme
  },
  data() {
    return {};
  },
  methods: {},
  mounted() {}
};
