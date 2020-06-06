import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'

import AccountLogin from '../views/Account/Login.vue'
import AccountRegister from '../views/Account/Register.vue'

import AccountManageProfile from '../views/Account/ManageProfile.vue'
import AccountManageEmail from '../views/Account/ManageEmail.vue'
import AccountManagePassword from '../views/Account/ManagePassword.vue'

import RestaurantCreate from '../views/Restaurant/Create.vue'
import RestaurantDetails from '../views/Restaurant/Details.vue'
import RestaurantEdit from '../views/Restaurant/Edit.vue'
import RestaurantDelete from '../views/Restaurant/Delete.vue'
import RestaurantIndex from '../views/Restaurant/Index.vue'

import PizzaTemplateCreate from '../views/PizzaTemplate/Create.vue'
import PizzaTemplateDetails from '../views/PizzaTemplate/Details.vue'
import PizzaTemplateEdit from '../views/PizzaTemplate/Edit.vue'
import PizzaTemplateDelete from '../views/PizzaTemplate/Delete.vue'
import PizzaTemplateIndex from '../views/PizzaTemplate/Index.vue'

import CategoryCreate from '../views/Category/Create.vue'
import CategoryDetails from '../views/Category/Details.vue'
import CategoryEdit from '../views/Category/Edit.vue'
import CategoryDelete from '../views/Category/Delete.vue'
import CategoryIndex from '../views/Category/Index.vue'

import RestaurantFoodCreate from '../views/RestaurantFood/Create.vue'
import RestaurantFoodDetails from '../views/RestaurantFood/Details.vue'
import RestaurantFoodEdit from '../views/RestaurantFood/Edit.vue'
import RestaurantFoodDelete from '../views/RestaurantFood/Delete.vue'
import RestaurantFoodIndex from '../views/RestaurantFood/Index.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    { path: '/', name: 'Home', component: Home },

    { path: '/account/login', name: 'AccountLogin', component: AccountLogin },
    { path: '/account/register', name: 'AccountRegitser', component: AccountRegister },
    
    { path: '/account/manage/profile', name: 'AccountManageProfile', component: AccountManageProfile },
    { path: '/account/manage/email', name: 'AccountManageEmail', component: AccountManageEmail },
    { path: '/account/manage/password', name: 'AccountManagePassword', component: AccountManagePassword },
    
    { path: '/restaurant', name: 'RestaurantIndex', component: RestaurantIndex },
    { path: '/restaurant/create', name: 'RestaurantCreate', component: RestaurantCreate },
    { path: '/restaurant/details/:id?', name: 'RestaurantDetails', component: RestaurantDetails, props: true },
    { path: '/restaurant/edit/:id?', name: 'RestaurantEdit', component: RestaurantEdit, props: true },
    { path: '/restaurant/delete/:id?', name: 'RestaurantDelete', component: RestaurantDelete, props: true },

    { path: '/pizzaTemplate', name: 'PizzaTemplateIndex', component: PizzaTemplateIndex },
    { path: '/pizzaTemplate/create', name: 'PizzaTemplateCreate', component: PizzaTemplateCreate },
    { path: '/pizzaTemplate/details/:id?', name: 'PizzaTemplateDetails', component: PizzaTemplateDetails, props: true },
    { path: '/pizzaTemplate/edit/:id?', name: 'PizzaTemplateEdit', component: PizzaTemplateEdit, props: true },
    { path: '/pizzaTemplate/delete/:id?', name: 'PizzaTemplateDelete', component: PizzaTemplateDelete, props: true },

    { path: '/category', name: 'CategoryIndex', component: CategoryIndex },
    { path: '/category/create', name: 'CategoryCreate', component: CategoryCreate },
    { path: '/category/details/:id?', name: 'CategoryDetails', component: CategoryDetails, props: true },
    { path: '/category/edit/:id?', name: 'CategoryEdit', component: CategoryEdit, props: true },
    { path: '/category/delete/:id?', name: 'CategoryDelete', component: CategoryDelete, props: true }
]

const router = new VueRouter({
    routes
})

export default router
