import { getAsync, postAsync, putAsync } from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/Grocery";

export async function getGroceriesAsync(colocId) {
  return getAsync(`${endpoint}/GetAllList/${colocId}`);
}
export async function createGroceryListAsync(model) {
  return postAsync(endpoint, model);
}
export async function getItemsAsync(groceryListId) {
  return getAsync(endpoint, groceryListId);
}
