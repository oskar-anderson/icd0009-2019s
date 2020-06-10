import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

import { RestaurantFoodService} from 'service/restaurantFood-service';
import { IRestaurantFood } from 'domain/IRestaurantFood';

import { PizzaService} from 'service/pizza-service';
import { IPizza } from 'domain/IPizza';

import { PizzaTemplateService} from 'service/pizzaTemplate-service';
import { IPizzaTemplate } from 'domain/IPizzaTemplate';

import { RestaurantService} from 'service/restaurant-service';
import { IRestaurant } from 'domain/IRestaurant';

import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class RestaurantFoodCreate {
    private _alert: IAlertData | null = null;

    private _pizzas?: IPizza[];
    private _restaurants?: IRestaurant[];

    private _date = new Date();

    private _restaurantFood: IRestaurantFood = {
        id: "",
        pizzaId: "",
        restaurantId: "",
        gross: 0,
    };

    constructor(private restaurantFoodService: RestaurantFoodService,
                private pizzaService: PizzaService,
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
                pizzaId: this._restaurantFood.pizzaId,
                restaurantId: this._restaurantFood.restaurantId,
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
