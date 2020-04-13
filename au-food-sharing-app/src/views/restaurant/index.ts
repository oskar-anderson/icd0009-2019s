import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IRestaurant } from './../../domain/IRestaurant';
import { RestaurantService } from 'service/restaurant-service';
import { IAlertData } from 'types/IAlertData';


@autoinject
export class RestaurantIndex{
    private _restaurants: IRestaurant[] = [];

    private _alert: IAlertData | null = null;

    constructor(private restaurantService: RestaurantService){

    }

    attached() {
        this.restaurantService.getRestaurants().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._restaurants = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
    }
}