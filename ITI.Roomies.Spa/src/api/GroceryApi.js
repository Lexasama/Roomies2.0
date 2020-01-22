import { getAsync, postAsync, putAsync } from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/Grocery";

export async function getGroceriesAsync(colocId) {
  return getAsync(`${endpoint}/GetAllList/${colocId}`);
}
