import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IComponent } from './../../domain/IComponent';
import { ComponentService } from 'service/component-service';
import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';


@autoinject
export class ComponentIndex{
    private _components: IComponent[] = [];

    private _alert: IAlertData | null = null;

    constructor(private componentService: ComponentService){

    }

    attached() {
        this.componentService.getComponents().then(
            response => {
                this._alert = alertHandler(SOURCE.COMPONENT, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._components = response.data!;
                    this._components.sort((a, b) => a.gross - b.gross)
                }
            }
        );
    }
}