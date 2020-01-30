import {
  getAsync,
  postAsync,
  putAsync,
  deleteAsync
} from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/item";

export async function getAllItemAsync() {
  return getAsync(`${endpoint}/getAllItems`);
}
export async function createItemAsync(model) {
  return postAsync(endpoint, model);
}
export async function deleteItemAsync(id) {
  return deleteAsync(`${endpoint}/${id}`);
}
export async function decreaseQuantityAsync(id) {
  return postAsync(`${endpoint}/decrease/${id}`);
}
export async function increaseQuantityAsync(id) {
  return postAsync(`${endpoint}/increase/${id}`);
}
