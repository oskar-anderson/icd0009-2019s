import Vue from 'vue'
import Vuex from 'vuex'

import { ILoginDTO } from '@/types/ILoginDTO'
import { AccountApi } from '@/services/AccountApi'
import { IRegisterDTO } from '@/types/IRegisterDTO';

import { IRestaurant } from './../domain/IRestaurant';
import { RestaurantApi } from '@/services/RestaurantApi';

import { ICategory } from './../domain/ICategory';
import { CategoryApi } from '@/services/CategoryApi';

import { IPizzaTemplate } from '../domain/IPizzaTemplate';
import { PizzaTemplateApi } from '@/services/PizzaTemplateApi';

import { IRestaurantFood } from './../domain/IRestaurantFood';
import { RestaurantFoodApi } from '@/services/RestaurantFoodApi';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        jwt: null as string | null,
        
        restaurants: [] as IRestaurant[],
        restaurant: null as IRestaurant | null,
        
        categorys: [] as ICategory[],
        category: null as ICategory | null,

        pizzaTemplates: [] as IPizzaTemplate[],
        pizzaTemplate: null as IPizzaTemplate | null,

        restaurantFoods: [] as IRestaurantFood[],
        restaurantFood: null as IRestaurantFood | null,
    },
    mutations: {
        setJwt(state, jwt: string | null) {
            state.jwt = jwt
        },


        setRestaurants(state, restaurants: IRestaurant[]) {
            state.restaurants = restaurants;
        },
        setRestaurant(state, restaurant: IRestaurant) {
            state.restaurant = restaurant;
        },


        setPizzaTemplates(state, pizzaTemplates: IPizzaTemplate[]) {
            state.pizzaTemplates = pizzaTemplates;
        },
        setPizzaTemplate(state, pizzaTemplate: IPizzaTemplate) {
            state.pizzaTemplate = pizzaTemplate;
        },


        setCategorys(state, categorys: ICategory[]) {
            state.categorys = categorys;
        },
        setCategory(state, category: ICategory) {
            state.category = category;
        },


        setRestaurantFoods(state, restaurantFoods: IRestaurantFood[]) {
            state.restaurantFoods = restaurantFoods;
        },
        setRestaurantFood(state, restaurantFood: IRestaurantFood) {
            state.restaurantFood = restaurantFood;
        }
    },
    getters: {
        isAuthenticated(context): boolean {
            return context.jwt !== null;
        }
    },
    actions: {
        clearJwt(context): void{
            context.commit('setJwt', null);
        },
        async authenticateUser(context, loginDTO: ILoginDTO): Promise<boolean> {
            const jwt = await AccountApi.getJwt(loginDTO);
            context.commit('setJwt', jwt);
            return jwt != null;
        },
        async registerUser(context, registerDTO: IRegisterDTO): Promise<boolean> {
            await AccountApi.register(registerDTO);
            await context.dispatch('getRestaurants');
            
            
            const jwt = await AccountApi.getJwt(registerDTO);
            context.commit('setJwt', jwt);
            return jwt != null;
        },
        
        // Restaurants

        async getRestaurants(context): Promise<void> {
            const restaurants = await RestaurantApi.getAll();
            context.commit('setRestaurants', restaurants);
            //await context.dispatch('getRestaurant');
        },
        async getRestaurant(context, id: string): Promise<void> {
            if (!id) {
                context.commit('setRestaurant', null);
            } else {
                const restaurant = await RestaurantApi.get(id);
                context.commit('setRestaurant', restaurant);
            }
        },
        async deleteRestaurant(context, id: string): Promise<void> {
            console.log('isAuthenticated for deleteRestaurant', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await RestaurantApi.delete(id, context.state.jwt);
                await context.dispatch('getRestaurants');
                //await context.dispatch('getRestaurant');
            } else {
                alert("Log in!");
            }
        },
        async editRestaurant(context, restaurant: IRestaurant): Promise<void> {
            console.log('isAuthenticated for editRestaurant', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await RestaurantApi.edit(restaurant, context.state.jwt);
                await context.dispatch('getRestaurants');
                //await context.dispatch('getRestaurant');
            } else {
                alert("Log in!");
            }
        },
        async createRestaurant(context, restaurant: IRestaurant): Promise<void> {
            console.log('isAuthenticated for createRestaurant', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await RestaurantApi.create(restaurant, context.state.jwt);
                await context.dispatch('getRestaurants');
                //await context.dispatch('getRestaurant');
            } else {
                alert("Log in!");
            }
        },

        // PizzaTemplates

        async getPizzaTemplates(context): Promise<void> {
            const pizzaTemplates = await PizzaTemplateApi.getAll();
            context.commit('setPizzaTemplates', pizzaTemplates);
            //await context.dispatch('getPizzaTemplate');
        },
        async getPizzaTemplate(context, id: string): Promise<void> {
            if (!id) {
                context.commit('setPizzaTemplate', null);
            } else {
                const pizzaTemplate = await PizzaTemplateApi.get(id);
                context.commit('setPizzaTemplate', pizzaTemplate);
            }
        },
        async deletePizzaTemplate(context, id: string): Promise<void> {
            console.log('isAuthenticated for deletePizzaTemplate', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await PizzaTemplateApi.delete(id, context.state.jwt);
                await context.dispatch('getPizzaTemplates');
                //await context.dispatch('getPizzaTemplate');
            } else {
                alert("Log in!");
            }
        },
        async editPizzaTemplate(context, pizzaTemplate: IPizzaTemplate): Promise<void> {
            console.log('isAuthenticated for editPizzaTemplate', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await PizzaTemplateApi.edit(pizzaTemplate, context.state.jwt);
                await context.dispatch('getPizzaTemplates');
                //await context.dispatch('getPizzaTemplate');
            } else {
                alert("Log in!");
            }
        },
        async createPizzaTemplate(context, pizzaTemplate: IPizzaTemplate): Promise<void> {
            console.log('isAuthenticated for createPizzaTemplate', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await PizzaTemplateApi.create(pizzaTemplate, context.state.jwt);
                await context.dispatch('getPizzaTemplates');
                //await context.dispatch('getPizzaTemplate');
            } else {
                alert("Log in!");
            }
        },

        // Categorys

        async getCategorys(context): Promise<void> {
            const category = await CategoryApi.getAll();
            context.commit('setCategorys', category);
            //await context.dispatch('getCategory');
        },
        async getCategory(context, id: string): Promise<void> {
            if (!id) {
                context.commit('setCategory', null);
            } else {
                const category = await CategoryApi.get(id);
                context.commit('setCategory', category);
            }
        },
        async deleteCategory(context, id: string): Promise<void> {
            console.log('isAuthenticated for deleteCategory', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await CategoryApi.delete(id, context.state.jwt);
                await context.dispatch('getCategorys');
                //await context.dispatch('getCategory');
            } else {
                alert("Log in!");
            }
        },
        async editCategory(context, category: ICategory): Promise<void> {
            console.log('isAuthenticated for editCategory', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await CategoryApi.edit(category, context.state.jwt);
                await context.dispatch('getCategorys');
                //await context.dispatch('getCategory');
            } else {
                alert("Log in!");
            }
        },
        async createCategory(context, category: ICategory): Promise<void> {
            console.log('isAuthenticated for createCategory', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await CategoryApi.create(category, context.state.jwt);
                await context.dispatch('getCategorys');
                //await context.dispatch('getCategory');
            } else {
                alert("Log in!");
            }
        },

        // RestaurantFoods

        async getRestaurantFoods(context): Promise<void> {
            const restaurantFood = await RestaurantFoodApi.getAll();
            context.commit('setRestaurantFoods', restaurantFood);
            //await context.dispatch('getRestaurantFood');
        },
        async getRestaurantFood(context, id: string): Promise<void> {
            if (!id) {
                context.commit('setRestaurantFood', null);
            } else {
                const restaurantFood = await RestaurantFoodApi.get(id);
                context.commit('setRestaurantFood', restaurantFood);
            }
        },
        async deleteRestaurantFood(context, id: string): Promise<void> {
            console.log('isAuthenticated for deleteRestaurantFood', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await RestaurantFoodApi.delete(id, context.state.jwt);
                await context.dispatch('getRestaurantFoods');
                //await context.dispatch('getRestaurantFood');
            } else {
                alert("Log in!");
            }
        },
        async editRestaurantFood(context, restaurantFood: IRestaurantFood): Promise<void> {
            console.log('isAuthenticated for editRestaurantFood', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await RestaurantFoodApi.edit(restaurantFood, context.state.jwt);
                await context.dispatch('getRestaurantFoods');
                //await context.dispatch('getRestaurantFood');
            } else {
                alert("Log in!");
            }
        },
        async createRestaurantFood(context, restaurantFood: IRestaurantFood): Promise<void> {
            console.log('isAuthenticated for createRestaurantFood', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await RestaurantFoodApi.create(restaurantFood, context.state.jwt);
                await context.dispatch('getRestaurantFoods');
                //await context.dispatch('getRestaurantFood');
            } else {
                alert("Log in!");
            }
        },



    },
    modules: {
    }
})
