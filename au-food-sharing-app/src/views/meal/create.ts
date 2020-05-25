import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

import { MealService} from 'service/meal-service';
import { IMeal } from 'domain/IMeal';

import { CategoryService} from 'service/category-service';
import { ICategory } from 'domain/ICategory';

import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class MealCreate {
    private _alert: IAlertData | null = null;

    private _categorys?: ICategory[];

    private _meal: IMeal = {
        id: '',
        categoryId: '',
        name: '',
        picture: '',
        description: "",
    };

    constructor(private mealService: MealService, private categoryService: CategoryService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.categoryService.getCategorys().then(
            response => {
                this._alert = alertHandler(SOURCE.MEAL, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._categorys = response.data!.filter(function (x) {
                        return x.forMeal;
                    })
                }
            }
        );
    }

    onSubmit(event: Event) {
        this.mealService
            .createMeal({
                categoryId: this._meal.categoryId,
                category: null,
                name: this._meal.name,
                picture: this._meal.picture,
                description: this._meal.description,
                })
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.MEAL, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.router.navigateToRoute('meal-index', {});
                    }
                }   
            );

        event.preventDefault();
    }

}
