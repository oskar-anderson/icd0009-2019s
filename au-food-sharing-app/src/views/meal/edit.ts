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
export class MealEdit {
    private _alert: IAlertData | null = null;

    private _meal: IMeal = {
        id: '',
        categoryId: '',
        name: '',
        picture: '',
        description: "",
    }
    

    private _categorys?: ICategory[];

    constructor(private mealService: MealService, private categoryService: CategoryService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.mealService.getMeal(params.id).then(
                response => {
                    this._alert = alertHandler(SOURCE.MEAL, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._meal = response.data!;
                    }
                }
            );
            this.categoryService.getCategorys().then(
                response => {
                    this._alert = alertHandler(SOURCE.CATEGORY, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._categorys = response.data!.filter(function (x) {
                            return x.forMeal;
                        })
                    }
                }
            );
        }
    }

    onSubmit(event: Event) {
        console.log(this._meal);
        this.mealService
            .updateMeal({
                id: this._meal.id,
                categoryId: this._meal.categoryId,
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
