import { getAsync, postAsync, deleteAsync } from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/task";

export async function getTaskAsync(taskId) {
  return await getAsync(`${endpoint}/getTask/${taskId}`);
}

export async function createTaskAsync(model) {
  return await postAsync(endpoint, model);
}

export async function getColocTaskList(colocId) {
  return await getAsync(`${endpoint}/getTasks/${colocId}`);
}

export async function getRoomieTaskList() {
  return await getAsync();
}

export async function deleteTaskAsync(taskId) {
  return await deleteAsync(`${endpoint}/${taskId}`);
}
