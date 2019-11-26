import {
  getAsync,
  postAsync,
  putAsync,
  deleteAsync
} from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/coloc";

export function getColocAsync(colocId) {
  return getAsync(`${endpoint}/${colocId}`);
}

export async function getColocListAsync(roomieId) {
  return await getAsync(`${endpoint}/colocList/${roomieId}`);
}
export function updateColocAsync(model) {
  return putAsync(`${endpoint}/${model.colocId}`, model);
}

export function createColocAsync(model) {
  return postAsync(endpoint, model);
}
