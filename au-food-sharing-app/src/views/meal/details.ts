import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { MealService } from 'service/meal-service';
import { IMeal } from 'domain/IMeal';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

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
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._meal = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this._meal = undefined;
                    }
                }                
            );
        }
    }

}
