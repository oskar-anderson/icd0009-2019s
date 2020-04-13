import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { OwnerService } from 'service/owner-service';
import { IOwner } from 'domain/IOwner';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class OwnersCreate {
    private _alert: IAlertData | null = null;


    _firstName = "";
    _lastName = ""

    constructor(private ownerService: OwnerService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.ownerService
            .createOwner({ firstName: this._firstName, lastName: this._lastName })
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
