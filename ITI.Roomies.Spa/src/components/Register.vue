<template>
  <div>
    <b-card>
      <b-form inline>
        <label for="select">Create an account as:</label>
        <b-form-select id="select" v-model="userType" :options="options" style="margin-left: 70px ">
          <template></template>
        </b-form-select>
      </b-form>
    </b-card>
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

