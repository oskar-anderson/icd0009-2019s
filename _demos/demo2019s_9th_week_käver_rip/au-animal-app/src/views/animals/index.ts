import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { IAnimal } from 'domain/IAnimal';
import { AnimalService } from 'service/animal-service';

@autoinject
export class AnimalsIndex {
    private _alert: IAlertData | null = null;
    private _animals: IAnimal[] = [];

    constructor(private animalService: AnimalService) {

    }

    attached() {
        this.animalService.getAnimals().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._animals = response.data!;
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
