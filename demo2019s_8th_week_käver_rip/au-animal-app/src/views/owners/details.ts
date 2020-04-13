import { IOwner } from 'domain/IOwner';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';

import { OwnerService } from 'service/owner-service';

@autoinject
export class OwnersDetails {

    private _owner: IOwner | null = null;

    constructor(private ownerService: OwnerService) {

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

}
