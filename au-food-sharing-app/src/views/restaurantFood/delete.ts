import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { RestaurantFoodService } from 'service/restaurantFood-service';
import { IRestaurantFood } from 'domain/IRestaurantFood';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';


@autoinject
export class RestaurantFoodDelete {
    private _alert: IAlertData | null = null;

    private _restaurantFood?: IRestaurantFood;

    constructor(private restaurantFoodService: RestaurantFoodService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.restaurantFoodService.getRestaurantFood(params.id).then(
                response => {
                    this._alert = alertHandler(SOURCE.RESTAURANTFOOD, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._restaurantFood = response.data!;
                    }
                }
            );
        }
    }

    onSubmit(event: Event) {
        this.restaurantFoodService
            .deleteRestaurantFood(this._restaurantFood!.id)
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
