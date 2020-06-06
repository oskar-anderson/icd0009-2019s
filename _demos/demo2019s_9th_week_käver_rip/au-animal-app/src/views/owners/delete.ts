import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { OwnerService } from 'service/owner-service';
import { IOwner } from 'domain/IOwner';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class OwnersDelete {
    private _alert: IAlertData | null = null;

    private _owner?: IOwner;

    constructor(private ownerService: OwnerService, private router: Router) {

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
                        };
                        this._owner = undefined;
                    }
                }
            );
        }
    }

    onSubmit(event: Event) {
        this.ownerService
            .deleteOwner(this._owner!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('owners-index', {});
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                }
            );
        event.preventDefault();
    }
}
