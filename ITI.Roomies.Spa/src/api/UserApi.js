import { getAsync } from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/roomie";

export async function getUserAsync() {
  return await getAsync(`${endpoint}/user`);
}
