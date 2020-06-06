import { IOwner } from 'domain/IOwner';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { OwnerService } from 'service/owner-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class OwnersDetails {

    private _owner?: IOwner;    
    private _alert: IAlertData | null = null;


    constructor(private ownerService: OwnerService) {

    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.ownerService.getOwner(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._owner = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this._owner = undefined;
                    }
                }                
            );
        }
    }

}
