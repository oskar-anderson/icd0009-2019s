import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';

import { ICategory } from './../../domain/ICategory';
import { IRestaurant } from './../../domain/IRestaurant';

import { CategoryService } from 'service/category-service';
import { RestaurantService } from 'service/restaurant-service';

import { IPizzaTemplate, IPizzaTemplateWithChildren } from './../../domain/IPizzaTemplate';
import { PizzaTemplateService } from 'service/pizzaTemplate-service';

import { IPizza, IPizzaWithRestaurants, Sizes } from './../../domain/IPizza';
import { PizzaService } from 'service/pizza-service';

import { RestaurantFoodService } from 'service/restaurantFood-service';
import { IRestaurantFood } from './../../domain/IRestaurantFood';

import { alertHandler, SOURCE } from 'service/alert-service';

import { IAlertData } from 'types/IAlertData';

@autoinject
export class HomeIndex{
    private _categorys: ICategory[] = [];
    private _restaurants: IRestaurant[] = [];
    private _pizzaTemplates: IPizzaTemplateWithChildren[] = [];
    private _pizzas: IPizzaWithRestaurants[] = [];
    private _displayedPizzaTemplates: IPizzaTemplateWithChildren[] = [];
    private _restaurantFoods: IRestaurantFood[] = [];

    private _categoryId: string | undefined = undefined;
    private _restaurantId = "";
    private _activeRestaurant: null | IRestaurant = null;
    private _optionState = 1;

    private _alert: IAlertData | null = null;

    constructor(
        private categoryService: CategoryService,
        private restaurantService: RestaurantService,
        private pizzaTemplateService: PizzaTemplateService, 
        private pizzaService: PizzaService,
        private restaurantFoodService: RestaurantFoodService){

    }

    attached() {
        this.pizzaTemplateService.getPizzaTemplates().then(
            response => {
                this._alert = alertHandler(SOURCE.PIZZATEMPLATE, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._pizzaTemplates = response.data!

                    this.pizzaService.getPizzas().then(
                        response => {
                            this._alert = alertHandler(SOURCE.PIZZA, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                this._pizzas = response.data!;



                                this.restaurantFoodService.getRestaurantFoods().then(
                                    response => {
                                        this._alert = alertHandler(SOURCE.RESTAURANTFOOD, response.statusCode, response.errorMessage);
                                        if (response.statusCode >= 200 && response.statusCode < 300) {
                                            this._restaurantFoods = response.data!;
                                            this.getRestaurantFoodsToPizza();
                                        }
                                    }
                                )
                            }
                        }
                    )
                }
            }
        );
        this.categoryService.getCategorys().then(
            response => {
                this._alert = alertHandler(SOURCE.CATEGORY, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._categorys = response.data!;
                };
            }
        );
        this.restaurantService.getRestaurants().then(
            response => {
                this._alert = alertHandler(SOURCE.RESTAURANT, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._restaurants = response.data!;
                };
            }
        );
    }

    getRestaurantFoodsToPizza() {
        console.log("in getRestaurantFoodsToPizza")
        this._displayedPizzaTemplates = [];
        
        /*
        for (const restaurantFood of this._restaurantFoods) {
            let restaurant = this._restaurants.find(x => x.id === restaurantFood.restaurantId)!
            let pizzas = this._pizzas.filter(x => x.id === restaurantFood.pizzaId)
            for (const pizza of pizzas) {
                let PizzaTemplate = this._pizzaTemplates.find(x => x.id === pizza.pizzaTemplateId)!
                PizzaTemplate.pizzas.push(pizza)
                console.log(restaurant.name)
                console.log(pizza.name)
            }
        }
        */


        for (let pizzaTemplate of this._pizzaTemplates) {
            for (const pizza of pizzaTemplate.pizzas) {
                let restaurantFoods = this._restaurantFoods.filter(x => x.pizzaId == pizza.id);
                pizza.takenRestaurants = [...restaurantFoods];
            }
        }
    }

    categoryChanged(categoryId: string) {
        this._categoryId = categoryId;
        if (this._restaurantId !== "") {
            this.loadAfterFilterChange();
        }
    }

    restaurantChanged(restaurantId: string) {
        this._optionState = 2;
        this._restaurantId = restaurantId;
        if (this._categoryId !== "" || this._categoryId === undefined) {
            this._activeRestaurant = this._restaurants.find(x => x.id == restaurantId)!;
            this.loadAfterFilterChange();
        }
    }

    loadAfterFilterChange() {
        // filter category
        console.log("in loadAfterFilterChange");
        let categoryFilteredPizzaTemplates: IPizzaTemplateWithChildren[] = [];
        if (this._categoryId === undefined) {
            categoryFilteredPizzaTemplates = this._pizzaTemplates;
        } else {
            categoryFilteredPizzaTemplates = this._pizzaTemplates.filter(x => x.categoryId === this._categoryId);  
        }
        
        // filter restaurant
        let restaurantFoods = this._restaurantFoods.filter(x => x.restaurantId === this._restaurantId)
        let pizzas: IPizzaWithRestaurants[] = [];
        for (const restaurantFood of restaurantFoods) {
            let pizza = this._pizzas.find(x => x.id === restaurantFood.pizzaId)!;
            pizza.activeRestaurantFood = restaurantFood;
            pizzas.push(pizza);
        }
        this._displayedPizzaTemplates = [];
        for (const pizzaTemplate of categoryFilteredPizzaTemplates) {
            let pizzaChildren = pizzas.filter(x => x.pizzaTemplateId === pizzaTemplate.id);
            if (pizzaChildren.length !== 0) {
                // remove children whose price has not been set
                pizzaTemplate.pizzas = [...pizzaChildren];
                this._displayedPizzaTemplates.push(pizzaTemplate);
            }
        }
        
    }
}