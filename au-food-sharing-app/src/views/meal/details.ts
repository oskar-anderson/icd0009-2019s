import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { MealService } from 'service/meal-service';
import { IMeal } from 'domain/IMeal';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class MealDetails {

    private _meal?: IMeal;    
    private _alert: IAlertData | null = null;


    constructor(private mealService: MealService) {

    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.mealService.getMeal(params.id).then(
                response => {
                    this._alert = alertHandler(SOURCE.MEAL, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._meal = response.data!;
                    }
                }                
            );
        }
    }

}
