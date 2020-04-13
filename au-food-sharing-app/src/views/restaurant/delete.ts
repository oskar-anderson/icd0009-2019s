import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { RestaurantService } from 'service/restaurant-service';
import { IRestaurant } from 'domain/IRestaurant';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { RestaurantDetails } from './details';


@autoinject
export class MealDelete {
    private _alert: IAlertData | null = null;

    private _restaurant?: IRestaurant;

    constructor(private restaurantService: RestaurantService, private router: Router) {

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
                        };
                        this._restaurant = undefined;
                    }
                }
            );
        }
    }

    onSubmit(event: Event) {
        this.restaurantService
            .deleteRestaurant(this._restaurant!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('restaurant-index', {});
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
        event.preventDefault();
    }
}
