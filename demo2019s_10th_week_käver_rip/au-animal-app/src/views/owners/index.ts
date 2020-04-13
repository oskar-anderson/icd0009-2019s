import { AlertType } from './../../types/AlertType';
import { IOwner } from './../../domain/IOwner';
import { autoinject } from 'aurelia-framework';
import { OwnerService } from 'service/owner-service';
import { IAlertData } from 'types/IAlertData';

@autoinject
export class OwnersIndex {
    private _owners: IOwner[] = [];

    private _alert: IAlertData | null = null;

    constructor(private ownerService: OwnerService) {

    }

    attached() {
        this.ownerService.getOwners().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._owners = response.data!;
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
    }

}
