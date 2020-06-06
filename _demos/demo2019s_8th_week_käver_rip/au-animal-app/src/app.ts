import { autoinject, PLATFORM } from 'aurelia-framework';
import { RouterConfiguration, Router } from 'aurelia-router';

@autoinject
export class App {
    router?: Router;

    constructor() {

    }

    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;

        config.title = "Animals";

        config.map([
            { route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home' },

            { route: ['owners', 'owners/index'], name: 'owners-index', moduleId: PLATFORM.moduleName('views/owners/index'), nav: true, title: 'Owners' },
            { route: ['owners/details/:id'], name: 'owners-details', moduleId: PLATFORM.moduleName('views/owners/details'), nav: false, title: 'Owners Details' },
            { route: ['owners/edit/:id'], name: 'owners-edit', moduleId: PLATFORM.moduleName('views/owners/edit'), nav: false, title: 'Owners Edit' },
            { route: ['owners/delete/:id'], name: 'owners-delete', moduleId: PLATFORM.moduleName('views/owners/delete'), nav: false, title: 'Owners Delete' },
            { route: ['owners/create'], name: 'owners-create', moduleId: PLATFORM.moduleName('views/owners/create'), nav: false, title: 'Owners Create' },

            { route: ['animals', 'animals/index'], name: 'animals-index', moduleId: PLATFORM.moduleName('views/animals/index'), nav: true, title: 'Animals' },

            { route: ['owneranimals', 'owneranimals/index'], name: 'owneranimals-index', moduleId: PLATFORM.moduleName('views/owneranimals/index'), nav: true, title: 'OwnerAnimals' },
        ]
        );

        config.mapUnknownRoutes('views/home/index');
    }
}
