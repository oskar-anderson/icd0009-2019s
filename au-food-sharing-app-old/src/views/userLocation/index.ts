import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IUserLocation } from './../../domain/IUserLocation';
import { UserLocationService } from 'service/userLocation-service';
import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';


@autoinject
export class UserLocationIndex{
    private _userLocations: IUserLocation[] = [];

    private _alert: IAlertData | null = null;

    constructor(private userLocationService: UserLocationService){

    }

    attached() {
        this.userLocationService.getUserLocations().then(
            response => {
                this._alert = alertHandler(SOURCE.USERLOCATION, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._userLocations = response.data!;
                }
            }
        );
    }
}