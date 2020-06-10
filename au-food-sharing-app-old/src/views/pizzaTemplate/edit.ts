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
export class PizzaTemplateEdit {
    private _alert: IAlertData | null = null;
    
    private _pizzaTemplate: IPizzaTemplate = {
        id: '',
        categoryId: '',
        name: '',
        picture: '',
        modifications: 0,
        extras: 0,
        description: "",
        varietyState: 0,
    }
    
    private _categorys?: ICategory[];

    constructor(private pizzaTemplateService: PizzaTemplateService, private categoryService: CategoryService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.pizzaTemplateService.getPizzaTemplate(params.id).then(
                response => {
                    this._alert = alertHandler(SOURCE.PIZZATEMPLATE, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._pizzaTemplate = response.data!;
                    }
                }
            );
            this.categoryService.getCategorys().then(
                response => {
                    this._alert = alertHandler(SOURCE.CATEGORY, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._categorys = response.data;
                    }
                }
            );
        }
    }

    onSubmit(event: Event) {
        this.pizzaTemplateService
            .updatePizzaTemplate({
                id: this._pizzaTemplate.id,
                categoryId: this._pizzaTemplate.categoryId,
                name: this._pizzaTemplate.name,
                picture: this._pizzaTemplate.picture,
                modifications: parseInt(this._pizzaTemplate.modifications + ""),
                extras: parseInt(this._pizzaTemplate.extras + ""),
                description: this._pizzaTemplate.description,
                varietyState: parseInt(this._pizzaTemplate.varietyState + ""),
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
