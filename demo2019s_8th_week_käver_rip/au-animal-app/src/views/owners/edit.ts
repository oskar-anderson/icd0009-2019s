import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { OwnerService } from 'service/owner-service';
import { IOwner } from 'domain/IOwner';

@autoinject
export class OwnersEdit {

    private _owner: IOwner | null = null;

    constructor(private ownerService: OwnerService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.ownerService.getOwner(params.id).then(
                data => this._owner = data
            );
        }
    }

    onSubmit(event: Event) {
        console.log(event);
        this.ownerService
            .updateOwner(this._owner!)
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('owners-index', {});
            });

        event.preventDefault();
    }
}
