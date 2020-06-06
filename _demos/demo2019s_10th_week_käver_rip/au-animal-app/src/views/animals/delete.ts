import { AnimalService } from 'service/animal-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IAnimal } from 'domain/IAnimal';

@autoinject
export class OwnersDelete {
    private _alert: IAlertData | null = null;
    private _animal?: IAnimal;

    constructor(private animalService: AnimalService, private router: Router) {

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
                        };
                        this._animal = undefined;
                    }
                }
            );
        }
    }

    onSubmit(event: Event) {
        this.animalService
            .deleteAnimal(this._animal!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('animals-index', {});
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
