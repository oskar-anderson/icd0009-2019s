import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class OwnerAnimalsDelete {
    private _alert: IAlertData | null = null;
   

    constructor( private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }
}
