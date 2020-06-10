import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IPizza } from './../../domain/IPizza';
import { PizzaService } from 'service/pizza-service';
import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';


@autoinject
export class PizzaIndex{
    private _pizzas: IPizza[] = [];

    private _showTechnical: boolean = false;
    private _alert: IAlertData | null = null;

    constructor(private pizzaService: PizzaService){

    }

    attached() {
        this.pizzaService.getPizzas().then(
            response => {
                this._alert = alertHandler(SOURCE.PIZZA, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._pizzas = response.data!;
                }
            }
        );
    }

    TriggerTechnical(){
        this._showTechnical = !this._showTechnical;
    }
}