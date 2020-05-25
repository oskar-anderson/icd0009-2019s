import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { PizzaService } from 'service/pizza-service';
import { IPizza } from 'domain/IPizza';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';


@autoinject
export class PizzaDelete {
    private _alert: IAlertData | null = null;

    private _pizza?: IPizza;

    constructor(private pizzaService: PizzaService, private router: Router) {

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

    onSubmit(event: Event) {
        this.pizzaService
            .deletePizza(this._pizza!.id)
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.PIZZA, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.router.navigateToRoute('pizza-index', {});
                    }
                }
            );
        event.preventDefault();
    }
}
