import Vue from 'vue'
import VueRouter from 'vue-router'

import Home from '@/components/home/Home'
import Auth from '@/components/auth/Auth'
import AdminPages from '@/components/admin/AdminPages'

import {
    userKey
} from '@/global'

Vue.use(VueRouter)

const routes = [{
        name: 'home',
        path: '/',
        component: Home
    }, {
        name: 'auth',
        path: '/auth',
        component: Auth
    },
    {
        name: 'admin',
        path: '/admin',
        component: AdminPages
    }
]

const router = new VueRouter({
    mode: 'history',
    routes
})

router.beforeEach((to, from, next) => {

    if (to.name != "auth") {

        let token = JSON.parse(localStorage.getItem(userKey));

        var tokenExpiration = new Date(token.expiration);

        if (token.accessToken && new Date() < tokenExpiration) {
            next(true);
        } else {
            next({
                name: "auth"
            });
        }
    } else {
        next(true);
    }
})

export default router