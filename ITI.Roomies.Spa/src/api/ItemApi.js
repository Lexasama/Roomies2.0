import { getAsync, postAsync, putAsync } from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/item";

export async function getAllItemAsync() {
    return getAsync(`${endpoint}/getAllItems`);
}
