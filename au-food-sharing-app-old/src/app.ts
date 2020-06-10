import { AppState } from './state/app-state';
import { autoinject, PLATFORM } from 'aurelia-framework';
import { RouterConfiguration, Router } from 'aurelia-router'

import { AlertType } from './types/AlertType';
import { IAlertData } from 'types/IAlertData';

import { IRestaurant } from './domain/IRestaurant';
import { RestaurantService } from 'service/restaurant-service';

import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class App {
    router?: Router;

    private _alert: IAlertData | null = null;

    private: IAlertData | null = null;
    private _restaurants: IRestaurant[] = [];

    constructor(private appState: AppState, private restaurantService: RestaurantService) {

    }

    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;

        config.title = 'Pitsa Riina';

        config.map([
            {route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home' },
            

            {route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
            {route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },
            
            { route: ['account/manage'], name: 'account-manage', moduleId: PLATFORM.moduleName('views/account/manage/index'), nav: false, title: 'Manage Account' },
            { route: ['account/manageEmail'], name: 'account-manageEmail', moduleId: PLATFORM.moduleName('views/account/manage/email'), nav: false, title: 'Manage email' },
            { route: ['account/managePassword'], name: 'account-managePassword', moduleId: PLATFORM.moduleName('views/account/manage/password'), nav: false, title: 'Manage password' },
            

            {route: ['cart', 'cart/index'], name: 'cart-index', moduleId: PLATFORM.moduleName('views/cart/index'), nav: true, title: 'cart' },
            

            {route: ['category', 'category/index'], name: 'category-index', moduleId: PLATFORM.moduleName('views/category/index'), nav: true, title: 'category' },
            {route: ['category/create'], name: 'category-create', moduleId: PLATFORM.moduleName('views/category/create'), nav: false},
            {route: ['category/edit/:id?'], name: 'category-edit', moduleId: PLATFORM.moduleName('views/category/edit'), nav: false },
            {route: ['category/delete/:id?'], name: 'category-delete', moduleId: PLATFORM.moduleName('views/category/delete'), nav: false },
            

            {route: ['component', 'component/index'], name: 'component-index', moduleId: PLATFORM.moduleName('views/component/index'), nav: true, title: 'component' },
            {route: ['component/create'], name: 'component-create', moduleId: PLATFORM.moduleName('views/component/create'), nav: false},
            {route: ['component/edit/:id?'], name: 'component-edit', moduleId: PLATFORM.moduleName('views/component/edit'), nav: false },
            {route: ['component/delete/:id?'], name: 'component-delete', moduleId: PLATFORM.moduleName('views/component/delete'), nav: false },  

            

            {route: ['componentPizzaTPL', 'componentPizzaTPL/index'], name: 'componentPizzaTPL', moduleId: PLATFORM.moduleName('views/componentPizzaTPL/index'), nav: true, title: 'componentPizzaTPL' },
            

            {route: ['pizza', 'pizza/index'], name: 'pizza-index', moduleId: PLATFORM.moduleName('views/pizza/index'), nav: true, title: 'pizza' },
            {route: ['pizza/create'], name: 'pizza-create', moduleId: PLATFORM.moduleName('views/pizza/create'), nav: false},
            {route: ['pizza/edit/:id?'], name: 'pizza-edit', moduleId: PLATFORM.moduleName('views/pizza/edit'), nav: false },
            {route: ['pizza/delete/:id?'], name: 'pizza-delete', moduleId: PLATFORM.moduleName('views/pizza/delete'), nav: false },  
            
            
            {route: ['pizzaTemplate', 'pizzaTemplate/index'], name: 'pizzaTemplate-index', moduleId: PLATFORM.moduleName('views/pizzaTemplate/index'), nav: true, title: 'pizzaTemplate' },
            {route: ['pizzaTemplate/create'], name: 'pizzaTemplate-create', moduleId: PLATFORM.moduleName('views/pizzaTemplate/create'), nav: false },
            {route: ['pizzaTemplate/edit/:id?'], name: 'pizzaTemplate-edit', moduleId: PLATFORM.moduleName('views/pizzaTemplate/edit'), nav: false },
            {route: ['pizzaTemplate/delete/:id?'], name: 'pizzaTemplate-delete', moduleId: PLATFORM.moduleName('views/pizzaTemplate/delete'), nav: false },
            

            {route: ['restaurant', 'restaurant/index'], name: 'restaurant-index', moduleId: PLATFORM.moduleName('views/restaurant/index'), nav: true, title: 'restaurant' },
            {route: ['restaurant/create'], name: 'restaurant-create', moduleId: PLATFORM.moduleName('views/restaurant/create'), nav: false},
            {route: ['restaurant/edit/:id?'], name: 'restaurant-edit', moduleId: PLATFORM.moduleName('views/restaurant/edit'), nav: false },
            {route: ['restaurant/delete/:id?'], name: 'restaurant-delete', moduleId: PLATFORM.moduleName('views/restaurant/delete'), nav: false },
            
            
            {route: ['restaurantFood', 'restaurantFood/index'], name: 'restaurantFood-index', moduleId: PLATFORM.moduleName('views/restaurantFood/index'), nav: true, title: 'restaurantFood' },
            {route: ['restaurantFood/create'], name: 'restaurantFood-create', moduleId: PLATFORM.moduleName('views/restaurantFood/create'), nav: false},
            {route: ['restaurantFood/edit/:id?'], name: 'restaurantFood-edit', moduleId: PLATFORM.moduleName('views/restaurantFood/edit'), nav: false },
            {route: ['restaurantFood/delete/:id?'], name: 'restaurantFood-delete', moduleId: PLATFORM.moduleName('views/restaurantFood/delete'), nav: false },
            
            
            {route: ['sharing', 'sharing/index'], name: 'sharing-index', moduleId: PLATFORM.moduleName('views/sharing/index'), nav: true, title: 'sharing' },
            {route: ['sharing/edit/:id?'], name: 'sharing-edit', moduleId: PLATFORM.moduleName('views/sharing/edit'), nav: false },


            {route: ['userLocation', 'userLocation/index'], name: 'userLocation-index', moduleId: PLATFORM.moduleName('views/userLocation/index'), nav: true, title: 'userLocation' },
            {route: ['userLocation/create'], name: 'userLocation-create', moduleId: PLATFORM.moduleName('views/userLocation/create'), nav: false},
            {route: ['userLocation/edit/:id?'], name: 'userLocation-edit', moduleId: PLATFORM.moduleName('views/userLocation/edit'), nav: false },
            {route: ['userLocation/delete/:id?'], name: 'userLocation-delete', moduleId: PLATFORM.moduleName('views/userLocation/delete'), nav: false },
        ]
        );
        config.mapUnknownRoutes('views/home/index')
    }

    logoutOnClick(){
        this.appState.jwt = null;
        this.router!.navigateToRoute('account-login');
    }

    attached() {
        this.restaurantService.getRestaurants().then(
            response => {
                this._alert = alertHandler(SOURCE.RESTAURANT, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._restaurants = response.data!;
                }
            }
        );
    }
}
