import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { PizzaTemplateService } from 'service/pizzaTemplate-service';
import { IPizzaTemplate } from 'domain/IPizzaTemplate';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class PizzaTemplateDetails {

    private _pizzaTemplate?: IPizzaTemplate;    
    private _alert: IAlertData | null = null;


    constructor(private pizzaTemplateService: PizzaTemplateService) {

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

}
