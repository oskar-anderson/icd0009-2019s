import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { RestaurantService} from 'service/restaurant-service';
import { IRestaurant } from 'domain/IRestaurant';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class RestaurantCreate {
    private _alert: IAlertData | null = null;


    private _restaurant: IRestaurant = {
        id: '',
        name: '',
        location: '',
        telephone: "",
        openTime: "",
        openNotification: "",
    };

    constructor(private restaurantService: RestaurantService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.restaurantService
            .createRestaurant({
                name: this._restaurant.name,
                location: this._restaurant.location,
                telephone: this._restaurant.telephone,
                openTime: this._restaurant.openTime,
                openNotification: this._restaurant.openNotification,
                })
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.RESTAURANT, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.router.navigateToRoute('restaurant-index', {});
                    }
                }   
            );

        event.preventDefault();
    }

}
