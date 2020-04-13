import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { RestaurantService } from 'service/restaurant-service';
import { IRestaurant } from 'domain/IRestaurant';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class RestaurantDetails {

    private _restaurant?: IRestaurant;    
    private _alert: IAlertData | null = null;


    constructor(private restaurantService: RestaurantService) {

    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.restaurantService.getRestaurant(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._restaurant = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this._restaurant = undefined;
                    }
                }                
            );
        }
    }

}
