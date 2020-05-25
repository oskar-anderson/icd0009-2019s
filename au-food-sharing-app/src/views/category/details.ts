import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { CategoryService } from 'service/category-service';
import { ICategory } from 'domain/ICategory';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class CategoryDetails {

    private _category?: ICategory;    
    private _alert: IAlertData | null = null;


    constructor(private categoryService: CategoryService) {

    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.categoryService.getCategory(params.id).then(
                response => {
                    this._alert = alertHandler(SOURCE.CATEGORY, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._category = response.data!;
                    }
                }                
            );
        }
    }

}
