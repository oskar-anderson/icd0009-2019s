import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { PizzaTemplateService } from 'service/pizzaTemplate-service';
import { IPizzaTemplate } from 'domain/IPizzaTemplate';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';


@autoinject
export class PizzaTemplateDelete {
    private _alert: IAlertData | null = null;

    private _pizzaTemplate?: IPizzaTemplate;

    constructor(private pizzaTemplateService: PizzaTemplateService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.pizzaTemplateService.getPizzaTemplate(params.id).then(
                response => {
                    this._alert = alertHandler(SOURCE.PIZZATEMPLATE, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._pizzaTemplate = response.data!;
                    }
                }
            );
        }
    }

    onSubmit(event: Event) {
        this.pizzaTemplateService
            .deletePizzaTemplate(this._pizzaTemplate!.id)
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
