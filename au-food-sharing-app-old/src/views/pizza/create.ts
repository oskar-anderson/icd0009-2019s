import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

import { PizzaService} from 'service/pizza-service';
import { IPizza, Sizes } from 'domain/IPizza';

import { PizzaTemplateService} from 'service/pizzaTemplate-service';
import { IPizzaTemplate } from 'domain/IPizzaTemplate';

import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class PizzaCreate {
    private _alert: IAlertData | null = null;

    private _pizzaTemplates?: IPizzaTemplate[];

    private _sizes = Sizes;
    private _sizeChoice = 0;

    private _pizza: IPizza = {
        id: '',
        pizzaTemplateId: '',
        sizeNumber: 0,
        sizeName: '',
        name: '',
    }

    constructor(private pizzaService: PizzaService, private pizzaTemplateService: PizzaTemplateService, private router: Router) {

    }

    dropdownChanged() {
        let size = this._sizes[parseInt(this._sizeChoice + "")].valueName;
        let yourSelect = document.getElementById("PizzaTemplateId") as HTMLSelectElement;
        let pizzaTemplate = yourSelect.options[yourSelect.selectedIndex].value
        this._pizza.name = size === "" ? pizzaTemplate : pizzaTemplate + " - " + size;
    }

    attached() {

    }

    
    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.pizzaTemplateService.getPizzaTemplates().then(
            response => {
                this._alert = alertHandler(SOURCE.PIZZA, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._pizzaTemplates = response.data!;
                }
            }
        );
    }

    onSubmit(event: Event) {
        console.log(this._pizza.pizzaTemplateId)
        console.log(this._sizes[parseInt(this._sizeChoice + "")].size)
        console.log(this._sizes[parseInt(this._sizeChoice + "")].valueName)
        console.log(this._pizza.name)
        this.pizzaService
            .createPizza({
                pizzaTemplateId: this._pizza.pizzaTemplateId,
                pizzaTemplate: null,
                sizeNumber: this._sizes[parseInt(this._sizeChoice + "")].size,
                sizeName: this._sizes[parseInt(this._sizeChoice + "")].valueName,
                name: this._pizza.name,
                })
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
