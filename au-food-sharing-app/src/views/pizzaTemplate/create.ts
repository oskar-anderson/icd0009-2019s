import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

import { PizzaTemplateService} from 'service/pizzaTemplate-service';
import { IPizzaTemplate } from 'domain/IPizzaTemplate';

import { CategoryService} from 'service/category-service';
import { ICategory } from 'domain/ICategory';

import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class PizzaTemplateCreate {
    private _alert: IAlertData | null = null;

    private _categorys?: ICategory[];
    private _dummyImage = "https://dummyimage.com/300x300/ffffff/000000.png&text=No+image"

    private _pizzaTemplate: IPizzaTemplate = {
        id: '',
        categoryId: '',
        name: '',
        picture: '',
        modifications: 0,
        extras: 0,
        description: "",
        varietyState: 0,
    };



    constructor(private pizzaTemplateService: PizzaTemplateService, private categoryService: CategoryService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.categoryService.getCategorys().then(
            response => {
                this._alert = alertHandler(SOURCE.PIZZATEMPLATE, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._categorys = response.data;
                }
            }
        );
    }

    onSubmit(event: Event) {
        this.pizzaTemplateService
            .createPizzaTemplate({
                categoryId: this._pizzaTemplate.categoryId,
                category: null,
                name: this._pizzaTemplate.name,
                picture: this._pizzaTemplate.picture,
                modifications: this._pizzaTemplate.modifications,
                extras: this._pizzaTemplate.extras,
                description: this._pizzaTemplate.description,
                varietyState: this._pizzaTemplate.varietyState
                })
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.PIZZATEMPLATE, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.router.navigateToRoute('pizzaTemplate-index', {});
                    }
                }   
            );

        event.preventDefault();
    }

}
