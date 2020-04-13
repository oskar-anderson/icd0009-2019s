import { IOwner } from './../../domain/IOwner';
import { autoinject } from 'aurelia-framework';
import { OwnerService } from 'service/owner-service';

@autoinject
export class OwnersIndex {
    private _owners: IOwner[] = [];

    constructor(private ownerService: OwnerService) {

    }

    attached() {
        this.ownerService.getOwners().then(
            data => this._owners = data
        );
    }

}
