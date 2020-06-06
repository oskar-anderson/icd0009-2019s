import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'
import AccountLogin from '../views/Account/Login.vue'
import GpsSessionsIndex from '../views/GpsSessions/Index.vue'
import GpsSessionsDetails from '../views/GpsSessions/Details.vue'
import GpsLocationTypesIndex from '../views/GpsLocationTypes/Index.vue'
import GpsLocationsIndex from '../views/GpsLocations/Index.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    { path: '/', name: 'Home', component: Home },

    { path: '/account/login', name: 'AccountLogin', component: AccountLogin },

    { path: '/gpssessions', name: 'GpsSessions', component: GpsSessionsIndex },
    { path: '/gpssessions/details/:id?', name: 'GpsSessionsDetails', component: GpsSessionsDetails, props: true },

    { path: '/gpslocationtypes', name: 'GpsLocationTypes', component: GpsLocationTypesIndex },

    { path: '/gpslocations', name: 'GpsLocations', component: GpsLocationsIndex }
]

const router = new VueRouter({
    routes
})

export default router
