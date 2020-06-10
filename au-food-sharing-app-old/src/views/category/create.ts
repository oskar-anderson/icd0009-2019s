import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CategoryService } from 'service/category-service';
import { ICategory } from 'domain/ICategory';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class CategoryCreate {
    private _alert: IAlertData | null = null;


    private _category: ICategory = {
        id: '',
        name: '',
        countInRestaurant: 0,
    };

    constructor(private categoryService: CategoryService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.categoryService
            .createCategory({
                name: this._category.name,
                })
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.CATEGORY, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.router.navigateToRoute('category-index', {});
                    }
                }   
            );

        event.preventDefault();
    }

}
