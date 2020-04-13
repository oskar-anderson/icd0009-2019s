import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IMeal } from './../../domain/IMeal';
import { MealService } from 'service/meal-service';
import { IAlertData } from 'types/IAlertData';


@autoinject
export class MealIndex{
    private _meals: IMeal[] = [];

    private _alert: IAlertData | null = null;

    constructor(private mealService: MealService){

    }

    attached() {
        this.mealService.getMeals().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._meals = response.data!;
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