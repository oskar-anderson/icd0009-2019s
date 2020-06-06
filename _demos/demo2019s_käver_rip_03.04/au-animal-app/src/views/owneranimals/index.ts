import { OwnerAnimalService } from './../../service/owner-animal-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IOwnerAnimal } from 'domain/IOwnerAnimal';

@autoinject
export class OwnerAnimalsIndex {
    private _alert: IAlertData | null = null;
   
    private _ownerAnimals: IOwnerAnimal[] = [];

    constructor(private ownerAnimalService:OwnerAnimalService, private router: Router) {

    }

    attached() {
        this.ownerAnimalService.getOwnerAnimals().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._ownerAnimals = response.data!;
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

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }
}
