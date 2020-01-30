import { getAsync, postAsync, putAsync } from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/coloc";

export async function getColocAsync(colocId) {
  return getAsync(`${endpoint}/${colocId}`);
}

export async function getColocListAsync(roomieId) {
  return await getAsync(`${endpoint}/colocList/${roomieId}`);
}
export function updateColocAsync(model) {
  return putAsync(`${endpoint}`, model);
}

export function createColocAsync(model) {
  return postAsync(endpoint, model);
}

export function getColocByRoomieIdAsync(roomieId) {
  return getAsync(`${endpoint}/roomieColoc/${roomieId}`);
}

export function getColocPicAsync(colocId) {
  return getAsync(`${endpoint}/getPicture/${colocId}`);
}
