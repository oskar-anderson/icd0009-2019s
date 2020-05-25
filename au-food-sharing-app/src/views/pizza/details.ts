import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { PizzaService } from 'service/pizza-service';
import { IPizza } from 'domain/IPizza';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class PizzaDetails {

    private _pizza?: IPizza;    
    private _alert: IAlertData | null = null;


    constructor(private pizzaService: PizzaService) {

    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.pizzaService.getPizza(params.id).then(
                response => {
                    this._alert = alertHandler(SOURCE.PIZZA, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._pizza = response.data!;
                    }
                }                
            );
        }
    }

}
