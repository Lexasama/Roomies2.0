import {
  getAsync,
  postAsync,
  putAsync,
  deleteAsync
} from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/roomie";

export async function getRoomieAsync(roomieId) {
  return await getAsync(`${endpoint}/${roomieId}`);
}

export async function getRoomieProfileAsync() {
  return await getAsync(`${endpoint}/profile`);
}

export async function createRoomieAsync(model) {
  return await postAsync(endpoint, model);
}

export async function updateRoomieAsync(model) {
  return await putAsync(`${endpoint}/${model.roomieId}`, model);
}
