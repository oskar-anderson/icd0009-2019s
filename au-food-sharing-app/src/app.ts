import { AppState } from './state/app-state';
import { autoinject, PLATFORM } from 'aurelia-framework';
import { RouterConfiguration, Router } from 'aurelia-router'

@autoinject
export class App {
    public message: string = 'Hello World!';
    router?: Router;

    constructor(private appState: AppState) {

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
            {route: ['category', 'category/index'], name: 'category', moduleId: PLATFORM.moduleName('views/category/index'), nav: true, title: 'category' },
            {route: ['component', 'component/index'], name: 'component', moduleId: PLATFORM.moduleName('views/component/index'), nav: true, title: 'component' },
            {route: ['componentPrice', 'componentPrice/index'], name: 'componentPrice', moduleId: PLATFORM.moduleName('views/componentPrice/index'), nav: true, title: 'componentPrice' },
            {route: ['invoice', 'invoice/index'], name: 'invoice', moduleId: PLATFORM.moduleName('views/invoice/index'), nav: true, title: 'invoice' },
            {route: ['invoiceLine', 'invoiceLine/index'], name: 'invoiceLine', moduleId: PLATFORM.moduleName('views/invoiceLine/index'), nav: true, title: 'invoiceLine' },
            {route: ['item', 'item/index'], name: 'item', moduleId: PLATFORM.moduleName('views/item/index'), nav: true, title: 'item' },
            {route: ['meal', 'meal/index'], name: 'meal', moduleId: PLATFORM.moduleName('views/meal/index'), nav: true, title: 'meal' },
            {route: ['paymentMethod', 'paymentMethod/index'], name: 'paymentMethod', moduleId: PLATFORM.moduleName('views/paymentMethod/index'), nav: true, title: 'paymentMethod' },
            {route: ['person', 'person/index'], name: 'person', moduleId: PLATFORM.moduleName('views/person/index'), nav: true, title: 'person' },
            {route: ['pizzaComponent', 'pizzaComponent/index'], name: 'pizzaComponent', moduleId: PLATFORM.moduleName('views/pizzaComponent/index'), nav: true, title: 'pizzaComponent' },
            {route: ['pizza', 'pizza/index'], name: 'pizza', moduleId: PLATFORM.moduleName('views/pizza/index'), nav: true, title: 'pizza' },
            {route: ['pizzaFinal', 'pizzaFinal/index'], name: 'pizzaFinal', moduleId: PLATFORM.moduleName('views/pizzaFinal/index'), nav: true, title: 'pizzaFinal' },
            {route: ['pizzaTemplate', 'pizzaTemplate/index'], name: 'pizzaTemplate', moduleId: PLATFORM.moduleName('views/pizzaTemplate/index'), nav: true, title: 'pizzaTemplate' },
            
            {route: ['restaurant', 'restaurant/index'], name: 'restaurant-index', moduleId: PLATFORM.moduleName('views/restaurant/index'), nav: true, title: 'restaurant' },
            {route: ['restaurant/create'], name: 'restaurant-create', moduleId: PLATFORM.moduleName('views/restaurant/create'), nav: false},
            {route: ['restaurant/details/:id?'], name: 'restaurant-details', moduleId: PLATFORM.moduleName('views/restaurant/details'), nav: false },
            {route: ['restaurant/edit/:id?'], name: 'restaurant-edit', moduleId: PLATFORM.moduleName('views/restaurant/edit'), nav: false },
            {route: ['restaurant/delete/:id?'], name: 'restaurant-delete', moduleId: PLATFORM.moduleName('views/restaurant/delete'), nav: false },
            
            
            
            
            {route: ['restaurantFood', 'restaurantFood/index'], name: 'restaurantFood', moduleId: PLATFORM.moduleName('views/restaurantFood/index'), nav: true, title: 'restaurantFood' },
            {route: ['sharing', 'sharing/index'], name: 'sharing', moduleId: PLATFORM.moduleName('views/sharing/index'), nav: true, title: 'sharing' },
            {route: ['sharingItem', 'sharingItem/index'], name: 'sharingItem', moduleId: PLATFORM.moduleName('views/sharingItem/index'), nav: true, title: 'sharingItem' },
            {route: ['size', 'size/index'], name: 'size', moduleId: PLATFORM.moduleName('views/size/index'), nav: true, title: 'size' },
            {route: ['userLocation', 'userLocation/index'], name: 'userLocation', moduleId: PLATFORM.moduleName('views/userLocation/index'), nav: true, title: 'userLocation' },

        ]

        );
        config.mapUnknownRoutes('views/home/index')
    }

    logoutOnClick(){
        this.appState.jwt = null;
        this.router!.navigateToRoute('account-login');
    }
}
