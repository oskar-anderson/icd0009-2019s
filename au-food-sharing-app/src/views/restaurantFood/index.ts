import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

import { IRestaurantFood } from './../../domain/IRestaurantFood';
import { RestaurantFoodService } from 'service/restaurantFood-service';

import { stringify } from 'querystring';
import { TextHandler } from 'aurelia-loader-nodejs';


@autoinject
export class RestaurantFoodIndex{
    private _restaurantFoods: IRestaurantFood[] = [];

    private _alert: IAlertData | null = null;
    private _showTechnical: boolean = false;

    constructor(
        private restaurantFoodService: RestaurantFoodService, 
        ){

    }

    attached() {
        this.restaurantFoodService.getRestaurantFoods().then(
            response => {
                this._alert = alertHandler(SOURCE.RESTAURANTFOOD, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._restaurantFoods = response.data!;
                }
            }
        );
    }

    TriggerTechnical(){
        this._showTechnical = !this._showTechnical;
    }
}