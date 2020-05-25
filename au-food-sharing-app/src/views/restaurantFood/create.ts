import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

import { RestaurantFoodService} from 'service/restaurantFood-service';
import { IRestaurantFood } from 'domain/IRestaurantFood';

import { PizzaService} from 'service/pizza-service';
import { IPizza } from 'domain/IPizza';

import { MealService} from 'service/meal-service';
import { IMeal } from 'domain/IMeal';

import { RestaurantService} from 'service/restaurant-service';
import { IRestaurant } from 'domain/IRestaurant';

import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class RestaurantFoodCreate {
    private _alert: IAlertData | null = null;

    private _pizzas?: IPizza[];
    private _meals?: IMeal[];
    private _restaurants?: IRestaurant[];

    private _date = new Date();

    private _restaurantFood: IRestaurantFood = {
        id: "",
        mealId: "",
        pizzaId: "",
        restaurantId: "",
        gross: 0,
        since: this._date.getDate() + "." + this._date.getMonth() + 1 + "." + this._date.getFullYear(),
        until: "01.01.9999",
    };

    constructor(private restaurantFoodService: RestaurantFoodService,
                private pizzaService: PizzaService,
                private mealService: MealService,
                private restaurantService: RestaurantService,
                private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.pizzaService.getPizzas().then(
            response => {
                this._alert = alertHandler(SOURCE.RESTAURANTFOOD, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._pizzas = response.data;
                }
            }
        );
        this.mealService.getMeals().then(
            response => {
                this._alert = alertHandler(SOURCE.RESTAURANTFOOD, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._meals = response.data;
                }
            }
        );
        this.restaurantService.getRestaurants().then(
            response => {
                this._alert = alertHandler(SOURCE.RESTAURANTFOOD, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._restaurants = response.data;
                }
            }
        );
    }

    onSubmit(event: Event) {
        this.restaurantFoodService
            .createRestaurantFood({
                mealId: this._restaurantFood.mealId,
                pizzaId: "",
                restaurantId: this._restaurantFood.restaurantId,
                gross: this._restaurantFood.gross,
                since: this._restaurantFood.since,
                until: this._restaurantFood.until,
                })
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.RESTAURANTFOOD, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.router.navigateToRoute('restaurantFood-index', {});
                    }
                }   
            );

        event.preventDefault();
    }

}
