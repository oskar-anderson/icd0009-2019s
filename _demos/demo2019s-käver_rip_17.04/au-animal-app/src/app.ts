import { AppState } from './state/app-state';
import { autoinject, PLATFORM } from 'aurelia-framework';
import { RouterConfiguration, Router } from 'aurelia-router';

@autoinject
export class App {
    router?: Router;

    constructor(private appState: AppState) {

    }

    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;

        config.title = "Animals";

        config.map([
            { route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home' },

            { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
            { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },


            { route: ['owners', 'owners/index'], name: 'owners-index', moduleId: PLATFORM.moduleName('views/owners/index'), nav: true, title: 'Owners' },
            { route: ['owners/details/:id?'], name: 'owners-details', moduleId: PLATFORM.moduleName('views/owners/details'), nav: false, title: 'Owners Details' },
            { route: ['owners/edit/:id?'], name: 'owners-edit', moduleId: PLATFORM.moduleName('views/owners/edit'), nav: false, title: 'Owners Edit' },
            { route: ['owners/delete/:id?'], name: 'owners-delete', moduleId: PLATFORM.moduleName('views/owners/delete'), nav: false, title: 'Owners Delete' },
            { route: ['owners/create'], name: 'owners-create', moduleId: PLATFORM.moduleName('views/owners/create'), nav: false, title: 'Owners Create' },

            { route: ['animals', 'animals/index'], name: 'animals-index', moduleId: PLATFORM.moduleName('views/animals/index'), nav: true, title: 'Animals' },
            { route: ['animals/details/:id?'], name: 'animals-details', moduleId: PLATFORM.moduleName('views/animals/details'), nav: false, title: 'Animals Details' },
            { route: ['animals/edit/:id?'], name: 'animals-edit', moduleId: PLATFORM.moduleName('views/animals/edit'), nav: false, title: 'Animals Edit' },
            { route: ['animals/delete/:id?'], name: 'animals-delete', moduleId: PLATFORM.moduleName('views/animals/delete'), nav: false, title: 'Animals Delete' },
            { route: ['animals/create'], name: 'animals-create', moduleId: PLATFORM.moduleName('views/animals/create'), nav: false, title: 'Animals Create' },


            { route: ['owneranimals', 'owneranimals/index'], name: 'owneranimals-index', moduleId: PLATFORM.moduleName('views/owneranimals/index'), nav: true, title: 'OwnerAnimals' },
            { route: ['owneranimals/details/:id?'], name: 'owneranimals-details', moduleId: PLATFORM.moduleName('views/owneranimals/details'), nav: false, title: 'OwnerAnimals Details' },
            { route: ['owneranimals/edit/:id?'], name: 'owneranimals-edit', moduleId: PLATFORM.moduleName('views/owneranimals/edit'), nav: false, title: 'OwnerAnimals Edit' },
            { route: ['owneranimals/delete/:id?'], name: 'owneranimals-delete', moduleId: PLATFORM.moduleName('views/owneranimals/delete'), nav: false, title: 'OwnerAnimals Delete' },
            { route: ['owneranimals/create'], name: 'owneranimals-create', moduleId: PLATFORM.moduleName('views/owneranimals/create'), nav: false, title: 'OwnerAnimals Create' },
        ]
        );

        config.mapUnknownRoutes('views/home/index');
    }

    logoutOnClick(){
        this.appState.jwt = null;
        this.router!.navigateToRoute('account-login');
    }

}
