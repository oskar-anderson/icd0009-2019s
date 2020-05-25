import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IMeal } from './../../domain/IMeal';
import { MealService } from 'service/meal-service';
import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';


@autoinject
export class MealIndex{
    private _meals: IMeal[] = [];

    private _alert: IAlertData | null = null;

    constructor(private mealService: MealService){

    }

    attached() {
        this.mealService.getMeals().then(
            response => {
                this._alert = alertHandler(SOURCE.MEAL, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._meals = response.data!;
                }
            }
        );
    }
}