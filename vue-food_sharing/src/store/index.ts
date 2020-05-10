import Vue from 'vue'
import Vuex from 'vuex'

import { ILoginDTO } from '@/types/ILoginDTO'
import { AccountApi } from '@/services/AccountApi'
import { IRegisterDTO } from '@/types/IRegisterDTO';

import { IRestaurant } from './../domain/IRestaurant';
import { RestaurantApi } from '@/services/RestaurantApi';

import { ICategory } from './../domain/ICategory';
import { CategoryApi } from '@/services/CategoryApi';

import { IMeal } from './../domain/IMeal';
import { MealApi } from '@/services/MealApi';

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

        meals: [] as IMeal[],
        meal: null as IMeal | null,

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


        setMeals(state, meals: IMeal[]) {
            state.meals = meals;
        },
        setMeal(state, meal: IMeal) {
            state.meal = meal;
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
            }
        },
        async editRestaurant(context, restaurant: IRestaurant): Promise<void> {
            console.log('isAuthenticated for editRestaurant', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await RestaurantApi.edit(restaurant, context.state.jwt);
                await context.dispatch('getRestaurants');
                //await context.dispatch('getRestaurant');
            }
        },
        async createRestaurant(context, restaurant: IRestaurant): Promise<void> {
            console.log('isAuthenticated for createRestaurant', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await RestaurantApi.create(restaurant, context.state.jwt);
                await context.dispatch('getRestaurants');
                //await context.dispatch('getRestaurant');
            }
        },

        // Meals

        async getMeals(context): Promise<void> {
            const meal = await MealApi.getAll();
            context.commit('setMeals', meal);
            //await context.dispatch('getMeal');
        },
        async getMeal(context, id: string): Promise<void> {
            if (!id) {
                context.commit('setMeal', null);
            } else {
                const meal = await MealApi.get(id);
                context.commit('setMeal', meal);
            }
        },
        async deleteMeal(context, id: string): Promise<void> {
            console.log('isAuthenticated for deleteMeal', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await MealApi.delete(id, context.state.jwt);
                await context.dispatch('getMeals');
                //await context.dispatch('getMeal');
            }
        },
        async editMeal(context, meal: IMeal): Promise<void> {
            console.log('isAuthenticated for editMeal', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await MealApi.edit(meal, context.state.jwt);
                await context.dispatch('getMeals');
                //await context.dispatch('getMeal');
            }
        },
        async createMeal(context, meal: IMeal): Promise<void> {
            console.log('isAuthenticated for createMeal', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await MealApi.create(meal, context.state.jwt);
                await context.dispatch('getMeals');
                //await context.dispatch('getMeal');
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
                const meal = await MealApi.get(id);
                context.commit('setCategory', meal);
            }
        },
        async deleteCategory(context, id: string): Promise<void> {
            console.log('isAuthenticated for deleteCategory', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await CategoryApi.delete(id, context.state.jwt);
                await context.dispatch('getCategorys');
                //await context.dispatch('getCategory');
            }
        },
        async editCategory(context, category: ICategory): Promise<void> {
            console.log('isAuthenticated for editCategory', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await CategoryApi.edit(category, context.state.jwt);
                await context.dispatch('getCategorys');
                //await context.dispatch('getCategory');
            }
        },
        async createCategory(context, category: ICategory): Promise<void> {
            console.log('isAuthenticated for createCategory', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await CategoryApi.create(category, context.state.jwt);
                await context.dispatch('getCategorys');
                //await context.dispatch('getCategory');
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
            }
        },
        async editRestaurantFood(context, restaurantFood: IRestaurantFood): Promise<void> {
            console.log('isAuthenticated for editRestaurantFood', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await RestaurantFoodApi.edit(restaurantFood, context.state.jwt);
                await context.dispatch('getRestaurantFoods');
                //await context.dispatch('getRestaurantFood');
            }
        },
        async createRestaurantFood(context, restaurantFood: IRestaurantFood): Promise<void> {
            console.log('isAuthenticated for createRestaurantFood', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await RestaurantFoodApi.create(restaurantFood, context.state.jwt);
                await context.dispatch('getRestaurantFoods');
                //await context.dispatch('getRestaurantFood');
            }
        },



    },
    modules: {
    }
})
