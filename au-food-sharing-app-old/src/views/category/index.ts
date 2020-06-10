import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { ICategory } from './../../domain/ICategory';
import { CategoryService } from 'service/category-service';
import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';


@autoinject
export class CategoryIndex{
    private _categorys: ICategory[] = [];

    private _alert: IAlertData | null = null;

    constructor(private categoryService: CategoryService){

    }

    attached() {
        this.categoryService.getCategorys().then(
            response => {
                this._alert = alertHandler(SOURCE.CATEGORY, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._categorys = response.data!;
                }
            }
        );
    }
}