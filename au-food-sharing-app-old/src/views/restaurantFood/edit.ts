import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

import { RestaurantFoodService} from 'service/restaurantFood-service';
import { IRestaurantFood } from 'domain/IRestaurantFood';

import { RestaurantService} from 'service/restaurant-service';
import { IRestaurant } from 'domain/IRestaurant';

import { PizzaService} from 'service/pizza-service';
import { IPizza } from 'domain/IPizza';

import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class RestaurantFoodEdit {
    private _alert: IAlertData | null = null;
    
    private _restaurantFood: IRestaurantFood = {
        id: '',
        restaurantId: '',
        pizzaId: '',
        gross: 0,
    }
    
    private _restaurants?: IRestaurant[];
    private _pizzas?: IPizza[];

    constructor(
        private restaurantFoodService: RestaurantFoodService, 
        private restaurantService: RestaurantService, 
        private pizzaService: PizzaService,
        private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.restaurantFoodService.getRestaurantFood(params.id).then(
                response => {
                    this._alert = alertHandler(SOURCE.RESTAURANTFOOD, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._restaurantFood = response.data!;
                    }
                }
            );
            this.restaurantService.getRestaurants().then(
                response => {
                    this._alert = alertHandler(SOURCE.RESTAURANT, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._restaurants = response.data;
                    }
                }
            );
            this.pizzaService.getPizzas().then(
                response => {
                    this._alert = alertHandler(SOURCE.PIZZA, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._pizzas = response.data;
                    }
                }
            );
        }
    }

    onSubmit(event: Event) {
        this.restaurantFoodService
            .updateRestaurantFood({
                id: this._restaurantFood.id,
                restaurantId: this._restaurantFood.restaurantId,
                pizzaId: this._restaurantFood.pizzaId,
                gross: parseFloat(this._restaurantFood.gross + ""),
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
