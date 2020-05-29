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
    public message: string = 'Hello World!';
    router?: Router;

    private _alert: IAlertData | null = null;

    private: IAlertData | null = null;
    private _restaurants: IRestaurant[] = [];

    constructor(private appState: AppState, private restaurantService: RestaurantService) {

    }

    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;

        config.title = 'food-sharing';

        config.map([
            {route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home' },
            
            {route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
            {route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },
            
            {route: ['cart', 'cart/index'], name: 'cart', moduleId: PLATFORM.moduleName('views/cart/index'), nav: true, title: 'cart' },
            {route: ['cartMeal', 'cartMeal/index'], name: 'cartMeal', moduleId: PLATFORM.moduleName('views/cartMeal/index'), nav: true, title: 'cartMeal' },
            
            {route: ['category', 'category/index'], name: 'category-index', moduleId: PLATFORM.moduleName('views/category/index'), nav: true, title: 'category' },
            {route: ['category/create'], name: 'category-create', moduleId: PLATFORM.moduleName('views/category/create'), nav: false},
            {route: ['category/details/:id?'], name: 'category-details', moduleId: PLATFORM.moduleName('views/category/details'), nav: false },
            {route: ['category/edit/:id?'], name: 'category-edit', moduleId: PLATFORM.moduleName('views/category/edit'), nav: false },
            {route: ['category/delete/:id?'], name: 'category-delete', moduleId: PLATFORM.moduleName('views/category/delete'), nav: false },
            
            
            
            {route: ['component', 'component/index'], name: 'component-index', moduleId: PLATFORM.moduleName('views/component/index'), nav: true, title: 'component' },
            {route: ['component/create'], name: 'component-create', moduleId: PLATFORM.moduleName('views/component/create'), nav: false},
            {route: ['component/edit/:id?'], name: 'component-edit', moduleId: PLATFORM.moduleName('views/component/edit'), nav: false },
            {route: ['component/delete/:id?'], name: 'component-delete', moduleId: PLATFORM.moduleName('views/component/delete'), nav: false },  

            
            {route: ['componentPizzaTPL', 'componentPizzaTPL/index'], name: 'componentPizzaTPL', moduleId: PLATFORM.moduleName('views/componentPizzaTPL/index'), nav: true, title: 'componentPizzaTPL' },
            {route: ['componentPizzaUser', 'componentPizzaUser/index'], name: 'componentPizzaUser', moduleId: PLATFORM.moduleName('views/componentPizzaUser/index'), nav: true, title: 'componentPizzaUser' },
            {route: ['item', 'item/index'], name: 'item', moduleId: PLATFORM.moduleName('views/item/index'), nav: true, title: 'item' },
            
            {route: ['pizza', 'pizza/index'], name: 'pizza-index', moduleId: PLATFORM.moduleName('views/pizza/index'), nav: true, title: 'pizza' },
            {route: ['pizza/create'], name: 'pizza-create', moduleId: PLATFORM.moduleName('views/pizza/create'), nav: false},
            {route: ['pizza/details/:id?'], name: 'pizza-details', moduleId: PLATFORM.moduleName('views/pizza/details'), nav: false },
            {route: ['pizza/edit/:id?'], name: 'pizza-edit', moduleId: PLATFORM.moduleName('views/pizza/edit'), nav: false },
            {route: ['pizza/delete/:id?'], name: 'pizza-delete', moduleId: PLATFORM.moduleName('views/pizza/delete'), nav: false },  
            
            {route: ['pizzaUser', 'pizzaUser/index'], name: 'pizzaUser', moduleId: PLATFORM.moduleName('views/pizzaUser/index'), nav: true, title: 'pizzaUser' },
            
            {route: ['pizzaTemplate', 'pizzaTemplate/index'], name: 'pizzaTemplate-index', moduleId: PLATFORM.moduleName('views/pizzaTemplate/index'), nav: true, title: 'pizzaTemplate' },
            {route: ['pizzaTemplate/create'], name: 'pizzaTemplate-create', moduleId: PLATFORM.moduleName('views/pizzaTemplate/create'), nav: false },
            {route: ['pizzaTemplate/details/:id?'], name: 'pizzaTemplate-details', moduleId: PLATFORM.moduleName('views/pizzaTemplate/details'), nav: false},
            {route: ['pizzaTemplate/edit/:id?'], name: 'pizzaTemplate-edit', moduleId: PLATFORM.moduleName('views/pizzaTemplate/edit'), nav: false },
            {route: ['pizzaTemplate/delete/:id?'], name: 'pizzaTemplate-delete', moduleId: PLATFORM.moduleName('views/pizzaTemplate/delete'), nav: false },
            
            {route: ['restaurant', 'restaurant/index'], name: 'restaurant-index', moduleId: PLATFORM.moduleName('views/restaurant/index'), nav: true, title: 'restaurant' },
            {route: ['restaurant/create'], name: 'restaurant-create', moduleId: PLATFORM.moduleName('views/restaurant/create'), nav: false},
            {route: ['restaurant/details/:id?'], name: 'restaurant-details', moduleId: PLATFORM.moduleName('views/restaurant/details'), nav: false },
            {route: ['restaurant/edit/:id?'], name: 'restaurant-edit', moduleId: PLATFORM.moduleName('views/restaurant/edit'), nav: false },
            {route: ['restaurant/delete/:id?'], name: 'restaurant-delete', moduleId: PLATFORM.moduleName('views/restaurant/delete'), nav: false },
            
            
            {route: ['restaurantFood', 'restaurantFood/index'], name: 'restaurantFood-index', moduleId: PLATFORM.moduleName('views/restaurantFood/index'), nav: true, title: 'restaurantFood' },
            {route: ['restaurantFood/create'], name: 'restaurantFood-create', moduleId: PLATFORM.moduleName('views/restaurantFood/create'), nav: false},
            {route: ['restaurantFood/edit/:id?'], name: 'restaurantFood-edit', moduleId: PLATFORM.moduleName('views/restaurantFood/edit'), nav: false },
            {route: ['restaurantFood/delete/:id?'], name: 'restaurantFood-delete', moduleId: PLATFORM.moduleName('views/restaurantFood/delete'), nav: false },
            
            
            
            {route: ['sharing', 'sharing/index'], name: 'sharing', moduleId: PLATFORM.moduleName('views/sharing/index'), nav: true, title: 'sharing' },
            {route: ['sharingItem', 'sharingItem/index'], name: 'sharingItem', moduleId: PLATFORM.moduleName('views/sharingItem/index'), nav: true, title: 'sharingItem' },
            {route: ['userLocation', 'userLocation/index'], name: 'userLocation', moduleId: PLATFORM.moduleName('views/userLocation/index'), nav: true, title: 'userLocation' },

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
                this._alert = alertHandler(SOURCE.APP, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._restaurants = response.data!;
                }
            }
        );
    }
}
