import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { OwnerService } from 'service/owner-service';
import { IOwner } from 'domain/IOwner';

@autoinject
export class OwnersCreate {

    _firstName = "";
    _lastName = ""

    constructor(private ownerService: OwnerService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.ownerService
            .createOwner({ firstName: this._firstName, lastName: this._lastName, animalCount: 0, id: '' })
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('owners-index', {});
            });

        event.preventDefault();
    }

}
