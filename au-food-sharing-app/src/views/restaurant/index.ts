import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IRestaurant } from './../../domain/IRestaurant';
import { RestaurantService } from 'service/restaurant-service';
import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';


@autoinject
export class RestaurantIndex{
    private _restaurants: IRestaurant[] = [];

    private _alert: IAlertData | null = null;

    constructor(private restaurantService: RestaurantService){

    }

    attached() {
        this.restaurantService.getRestaurants().then(
            response => {
                this._alert = alertHandler(SOURCE.RESTAURANT, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._restaurants = response.data!;
                }
            }
        );
    }
}