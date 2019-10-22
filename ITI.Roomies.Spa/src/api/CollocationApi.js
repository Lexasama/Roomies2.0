import { getAsync, postAsync, putAsync, deleteAsync, getStringAsync } from '../helpers/apiHelper'

const endpoint = process.env.VUE_APP_BACKEND + "/api/Collocation";

export async function createCollocAsync(collocname){
    return await postAsync(endpoint, collocname);
}

export async function getCollocNameIdByRoomieIdAsync() {
    return await getAsync(`${endpoint}/getNameId`);
}

export async function GetRoomiesIdNamesByCollocIdAsync(collocId) {
    return await getAsync(`${endpoint}/getRoomieIdNames/${collocId}`);
};

export async function quitCollocAsync(collocId){
    return await deleteAsync(`${endpoint}/quitColloc/${collocId}`);
};

export async function InviteAsync(email, collocId){
    return await postAsync(`${endpoint}/${email}/invite/${collocId}`)
};

export async function JoinAsync(invitationKey){
    return await postAsync(`${endpoint}/join/${invitationKey}`);
};

export async function getCollocInformation(collocId){
    return await getAsync(`${endpoint}/getCollocInformation/${collocId}`)
};

export async function DestroyCollocAsync(collocId){
    return await deleteAsync(`${endpoint}/DestroyCollocAsync/${collocId}`)
};

export async function IsAdminAsync(collocId){
    return await getAsync(`${endpoint}/IsAdminAsync/${collocId}`)
};

export async function getCollocPic( collocId) {
    return await getStringAsync(`${endpoint}/getRoomiePic/${collocId}`);
}