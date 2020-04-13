import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { MealService } from 'service/meal-service';
import { IMeal } from 'domain/IMeal';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class MealDelete {
    private _alert: IAlertData | null = null;

    private _meal?: IMeal;

    constructor(private mealService: MealService, private router: Router) {

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
                        };
                        this._meal = undefined;
                    }
                }
            );
        }
    }

    onSubmit(event: Event) {
        this.mealService
            .deleteMeal(this._meal!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('meal-index', {});
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
