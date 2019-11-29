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

export async function findRoomieByEmailAsync(email) {
  return await getAsync(`${endpoint}/getRoomieByEmail/${email}`);
}

export async function findUserByEmailAsync() {
  return await getAsync(`${endpoint}/getUserByEmail`);
}
export async function getRoomieProfileAsync(roomieId) {
  return await getAsync(`${endpoint}/profile/${roomieId}`);
}

// export async function getMyProfileAsync(roomieId) {
//   return await getAsync(`${endpoint}/profile/${roomieId}`);
// }

export async function createRoomieAsync(model) {
  return await postAsync(endpoint, model);
}

export async function updateRoomieAsync(model) {
  return await putAsync(endpoint, model);
}

// retrun the picture path of a coloc
export async function getPicAsync(roomieId) {
  return await getAsync(`${endpoint}/picure/${roomieId}`);
}

//returns the list of  roomies in a coloc
export async function getRoomiesAsync(colocId) {
  return await getAsync(`${endpoint}/getRoomies/${colocId}`);
}
