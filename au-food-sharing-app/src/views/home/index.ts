import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';

import { ICategory } from './../../domain/ICategory';
import { IRestaurant } from './../../domain/IRestaurant';

import { CategoryService } from 'service/category-service';
import { RestaurantService } from 'service/restaurant-service';

import { IAlertData } from 'types/IAlertData';

@autoinject
export class HomeIndex{
    private _categorys: ICategory[] = [];
    private _restaurants: IRestaurant[] = [];

    private _alert: IAlertData | null = null;

    constructor(private categoryService: CategoryService, private restaurantService: RestaurantService){

    }

    attached() {
        this.categoryService.getCategorys().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._categorys = response.data!;
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