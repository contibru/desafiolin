import Vue from 'vue'

export const userKey = '__desafiolin_user'
export const baseApiUrl = 'https://localhost:44398/api/'

export function showError(e) {
    if (e && e.response && e.response.data) {
        Vue.toasted.global.defaultError({
            msg: e.response.data
        })
    } else if (typeof e === 'string') {
        Vue.toasted.global.defaultError({
            msg: e
        })
    } else {
        Vue.toasted.global.defaultError()
    }
}

export default {
    baseApiUrl,
    showError,
    userKey
}