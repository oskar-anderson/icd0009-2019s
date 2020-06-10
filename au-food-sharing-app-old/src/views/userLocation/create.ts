import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { UserLocationService} from 'service/userLocation-service';
import { IUserLocation } from 'domain/IUserLocation';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class UserLocationCreate {
    private _alert: IAlertData | null = null;


    private _userLocation: IUserLocation = {
        id: '',
        district: '',
        streetName: '',
        buildingNumber: "",
        apartmentNumber: null,
    };

    constructor(private userLocationService: UserLocationService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        this.userLocationService
            .createUserLocation({
                district: this._userLocation.district,
                streetName: this._userLocation.streetName,
                buildingNumber: this._userLocation.buildingNumber,
                apartmentNumber: this._userLocation.apartmentNumber,
                })
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.USERLOCATION, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.router.navigateToRoute('userLocation-index', {});
                    }
                }   
            );

        event.preventDefault();
    }

}
