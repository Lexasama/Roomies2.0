import { getAsync, postAsync } from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/roomie";

export function getUserAsync() {
  return getAsync(`${endpoint}/user`);
}

export function uploadImage() {
  return postAsync(``);
}
