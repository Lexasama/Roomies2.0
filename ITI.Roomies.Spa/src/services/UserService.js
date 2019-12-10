const host = process.env.VUE_APP_BACKEND;
import { getUserAsync } from "@/api/UserApi";

class UserService {
  constructor() {
    this.userId = 0;
    this.userName = "";
    this.lastName = "";
    this.firstName = "";
    this.email = "";
    this.picPath = "";
  }
  setUserId(id) {
    this.userId = id;
  }
  setEmail(email) {
    if (email !== "") {
      this.email = email;
    }
  }
  setFirstName(name) {
    this.firstName = name;
  }
  setUserName(userName) {
    this.userName = userName;
  }
  setLastName(name) {
    this.lastName = name;
  }

  setPicPath(picPath) {
    this.picPath = picPath;
  }

  // get userId() {
  //   var id = this.userId;

  //   return id ? this.userId : null;
  // }
  // get email() {
  //   return this.email;
  // }

  get isValidUser() {
    var roomie = getUserAsync();
    if (roomie.userName !== null) {
      return true;
    } else {
      return false;
    }
  }

  async getUser() {
    let roomie = await getUserAsync();
    return roomie;
  }
}

export default new UserService();
