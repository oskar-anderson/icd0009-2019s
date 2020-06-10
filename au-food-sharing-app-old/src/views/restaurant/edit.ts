import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { RestaurantService} from 'service/restaurant-service';
import { IRestaurant } from 'domain/IRestaurant';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class RestaurantEdit {
    private _alert: IAlertData | null = null;

    private _restaurant?: IRestaurant;

    constructor(private restaurantService: RestaurantService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.restaurantService.getRestaurant(params.id).then(
                response => {
                    this._alert = alertHandler(SOURCE.RESTAURANT, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._restaurant = response.data!;
                    };
                }
            );
        }
    }

    onSubmit(event: Event) {
        console.log(this._restaurant);
        this.restaurantService
            .updateRestaurant(this._restaurant!)
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.RESTAURANT, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.router.navigateToRoute('restaurant-index', {});
                    };
                }
            );

        event.preventDefault();
    }
}
