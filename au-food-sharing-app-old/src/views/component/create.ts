import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { ComponentService} from 'service/component-service';
import { IComponent } from 'domain/IComponent';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class ComponentCreate {
    private _alert: IAlertData | null = null;


    private _component: IComponent = {
        id: '',
        name: '',
        gross: 0,
    };

    constructor(private componentService: ComponentService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.componentService
            .createComponent({
                name: this._component.name,
                gross: parseInt(this._component.gross + ""),
                })
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.COMPONENT, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.router.navigateToRoute('component-index', {});
                    }
                }   
            );

        event.preventDefault();
    }

}
