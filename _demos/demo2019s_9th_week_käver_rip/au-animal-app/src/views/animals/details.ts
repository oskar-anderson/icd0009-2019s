import { AnimalService } from 'service/animal-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IAnimal } from 'domain/IAnimal';

@autoinject
export class OwnersDetails {

    private _animal?: IAnimal;    
    private _alert: IAlertData | null = null;


    constructor(private animalService: AnimalService) {

    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.animalService.getAnimal(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._animal = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this._animal = undefined;
                    }
                }                
            );
        }
    }

}
