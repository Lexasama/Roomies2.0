import {
  getAsync,
  postAsync,
  putAsync,
  deleteAsync
} from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/task";

export async function createTask(model) {
  return await postAsync(endpoint, model);
}
