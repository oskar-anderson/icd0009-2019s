import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { MealService} from 'service/meal-service';
import { IMeal } from 'domain/IMeal';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class MealCreate {
    private _alert: IAlertData | null = null;


    private _meal: IMeal = {
        id: '',
        category: '',
        name: '',
        picture: '',
        description: "",
    };

    constructor(private mealService: MealService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.mealService
            .createMeal({
                category: this._meal.category,
                name: this._meal.name,
                picture: this._meal.picture,
                description: this._meal.description,
                })
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
