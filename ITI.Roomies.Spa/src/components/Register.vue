<template>
  <div>
    <div>
      <b-form-select v-model="userType" :options="options"></b-form-select>
    </div>
    <div v-if="userType == 'roomie'">
      <roomie />
    </div>
    <div v-if="userType == 'supervisor'">
      <supervisor />
    </div>
  </div>
</template>


<script>
import Roomie from "../components/Roomie/RegisterRoomie";
import Supervisor from "../components/Supervisor/SupervisorRegister";

export default {
  components: {
    Roomie,
    Supervisor
  },
  data() {
    return {
      userType: "roomie",
      options: [
        { value: "roomie", text: "Roomie" },
        { value: "supervisor", text: "Supervisor" }
      ]
    };
  }
};
</script>

