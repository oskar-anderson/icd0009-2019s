import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { UserLocationService } from 'service/userLocation-service';
import { IUserLocation } from 'domain/IUserLocation';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';


@autoinject
export class MealDelete {
    private _alert: IAlertData | null = null;

    private _userLocation?: IUserLocation;

    constructor(private userLocationService: UserLocationService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.userLocationService.getUserLocation(params.id).then(
                response => {
                    this._alert = alertHandler(SOURCE.USERLOCATION, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._userLocation = response.data!;
                    };
                }
            );
        }
    }

    onSubmit(event: Event) {
        this.userLocationService
            .deleteUserLocation(this._userLocation!.id)
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
