import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IComponentPizzaTemplate } from './../../domain/IComponentPizzaTemplate';
import { ComponentPizzaTemplateService } from 'service/componentPizzaTemplate-service';
import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';


@autoinject
export class ComponentPizzaTemplateIndex{
    private _componentPizzaTemplates: IComponentPizzaTemplate[] = [];

    private _alert: IAlertData | null = null;

    constructor(private componentPizzaTemplateService: ComponentPizzaTemplateService){

    }

    attached() {
        this.componentPizzaTemplateService.getComponentPizzaTemplates().then(
            response => {
                this._alert = alertHandler(SOURCE.COMPONENTPIZZATPL, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._componentPizzaTemplates = response.data!;
                }
            }
        );
    }
}