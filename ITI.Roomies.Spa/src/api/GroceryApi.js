import {
  getAsync,
  postAsync,
  putAsync,
  deleteAsync
} from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/Grocery";

export async function getGroceriesAsync(colocId) {
  return getAsync(`${endpoint}/GetAllList/${colocId}`);
}
export async function createGroceryListAsync(model) {
  return postAsync(endpoint, model);
}
export async function getItemsAsync(groceryListId) {
  return getAsync(`${endpoint}/getItems/${groceryListId}`);
}
export async function deleteGroceryListAsync(groceryListId) {
  return deleteAsync(`${endpoint}/${groceryListId}`);
}

export async function AddItemsAsync(model) {
  return postAsync(`${endpoint}/AddToList/`, model);
}
