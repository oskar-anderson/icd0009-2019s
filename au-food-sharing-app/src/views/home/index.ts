import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';

import { ICategory } from './../../domain/ICategory';
import { IRestaurant } from './../../domain/IRestaurant';

import { CategoryService } from 'service/category-service';
import { RestaurantService } from 'service/restaurant-service';

import { IPizzaTemplate, IPizzaTemplateWithChildren } from './../../domain/IPizzaTemplate';
import { PizzaTemplateService } from 'service/pizzaTemplate-service';

import { IPizza, IPizzaWithRestaurants, Sizes } from './../../domain/IPizza';
import { PizzaService } from 'service/pizza-service';

import { RestaurantFoodService } from 'service/restaurantFood-service';
import { IRestaurantFood } from './../../domain/IRestaurantFood';

import { ComponentPizzaTemplateService } from 'service/componentPizzaTemplate-service';
import { IComponentPizzaTemplate } from 'domain/IComponentPizzaTemplate';

import { ComponentService } from 'service/component-service';
import { IComponent } from 'domain/IComponent';

import { CartService } from 'service/cart-service';
import { ICart } from './../../domain/ICart';

import { CartMealService } from 'service/cartMeal-service';
import { ICartMeal } from './../../domain/ICartMeal';

import { UserLocationService } from 'service/userLocation-service';
import { IUserLocation } from 'domain/IUserLocation';

import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class HomeIndex{
    private _categorys: ICategory[] = [];
    private _restaurants: IRestaurant[] = [];
    private _pizzaTemplates: IPizzaTemplateWithChildren[] = [];
    private _pizzas: IPizzaWithRestaurants[] = [];
    private _restaurantFoods: IRestaurantFood[] = [];
    private _components: IComponent[] = [];
    private _componentPizzaTemplates: IComponentPizzaTemplate[] = [];
    private _cartMeals: ICartMeal[] = [];
    private _carts: ICart[] = [];
    private _userLocations: IUserLocation[] = [];

    private _displayedPizzaTemplates: IPizzaTemplateWithChildren[] = [];
    private _categoryId: string | undefined = undefined;
    private _activeRestaurant: null | IRestaurant = null;
    private _restaurantFilteredPizzaTemplates: IPizzaTemplateWithChildren[] = []; 
    private _createNewLocation: boolean = false;
    private _viewState: Number = 1;
    private _msg: string = "No message";


    private _userLocation: IUserLocation = {
        id: "",
        district: "",
        streetName: "",
        buildingNumber: "",
        apartmentNumber: "",
    }

    private _cartMeal: ICartMeal = {
        id: "",
        cartId: "",
        pizzaId: "",
        name: "",
        pizzaGross: 0,
        changes: "",
        componentsGross: 0,
        totalGross: 0
    };

    private _cart: ICart = {
        id: "",
        restaurantId: "",
        asDelivery: false,
        userLocationId: null,
        paymentMethod: "",
        firstName: "",
        lastName: "",
        phone: "",
    };

    private _alert: IAlertData | null = null;

    constructor(
        private categoryService: CategoryService,
        private restaurantService: RestaurantService,
        private pizzaTemplateService: PizzaTemplateService, 
        private pizzaService: PizzaService,
        private restaurantFoodService: RestaurantFoodService,
        private componentPizzaTemplateService: ComponentPizzaTemplateService,
        private componentService: ComponentService,
        private cartService: CartService,
        private cartMealService: CartMealService,
        private userLocationService: UserLocationService){

    }

    attached() {
        this._msg = localStorage.getItem("msg") !== null? localStorage.getItem("msg")! : "";
        localStorage.setItem("msg", "");
        this.pizzaTemplateService.getPizzaTemplates().then(
            response => {
                this._alert = alertHandler(SOURCE.PIZZATEMPLATE, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._pizzaTemplates = response.data!


                    this.pizzaService.getPizzas().then(
                        response => {
                            this._alert = alertHandler(SOURCE.PIZZA, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                this._pizzas = response.data!;
                                for (const pizzaTemplate of this._pizzaTemplates) {
                                    let pizzas = this._pizzas.filter(x => x.pizzaTemplateId === pizzaTemplate.id)
                                    pizzaTemplate.pizzas = [];
                                    for (const pizza of pizzas) {
                                        pizzaTemplate.pizzas.push(pizza);
                                        if (pizzaTemplate.pizzas.indexOf(pizza) === 0) {
                                            pizzaTemplate.selectedPizza = pizza;
                                            pizza.isSelectedPizza = true;
                                        } 
                                        else {
                                            pizza.isSelectedPizza = false;
                                        }
                                    }
                                    pizzaTemplate.componentsAreLoaded = false;
                                    pizzaTemplate.currentExtras = 0;
                                    pizzaTemplate.currentModifications = 0;
                                }


                                this.restaurantFoodService.getRestaurantFoods().then(
                                    response => {
                                        this._alert = alertHandler(SOURCE.RESTAURANTFOOD, response.statusCode, response.errorMessage);
                                        if (response.statusCode >= 200 && response.statusCode < 300) {
                                            this._restaurantFoods = response.data!;
                                            this.setRestaurantFoodsToPizza();
                                        }
                                    }
                                )
                            }
                        }
                    )
                    this.componentService.getComponents().then(
                        response => {
                            this._alert = alertHandler(SOURCE.COMPONENT, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                this._components = response.data!;
            
                                this.componentPizzaTemplateService.getComponentPizzaTemplates().then(
                                    response => {
                                        this._alert = alertHandler(SOURCE.COMPONENTPIZZATPL, response.statusCode, response.errorMessage);
                                        if (response.statusCode >= 200 && response.statusCode < 300) {
                                            this._componentPizzaTemplates = response.data!;
                                            console.log("calling setComponents");
                                            this.setComponents();
                                        }
                                    }
                                );
                            }
                        }
                    );
                }
            }
        );
        this.categoryService.getCategorys().then(
            response => {
                this._alert = alertHandler(SOURCE.CATEGORY, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._categorys = response.data!;
                    for (const category of this._categorys) {
                        category.countInRestaurant = 0;
                    }
                };
            }
        );
        this.restaurantService.getRestaurants().then(
            response => {
                this._alert = alertHandler(SOURCE.RESTAURANT, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._restaurants = response.data!;
                };
            }
        );
        this.userLocationService.getUserLocations().then(
            response => {
                this._alert = alertHandler(SOURCE.USERLOCATION, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._userLocations = response.data!;
                    for (const userLocation of this._userLocations) {
                        console.log(userLocation.streetName);
                    }
                    this._createNewLocation = this._userLocations.length === 0;
                };
            }
        );
    }

    setRestaurantFoodsToPizza() {
        for (let pizzaTemplate of this._pizzaTemplates) {
            for (const pizza of pizzaTemplate.pizzas) {
                let restaurantFoods = this._restaurantFoods.filter(x => x.pizzaId == pizza.id);
                pizza.takenRestaurants = [...restaurantFoods];
            }
        }
    }

    setComponents() {
        for (let pizzaTemplate of this._pizzaTemplates.filter(x => x.varietyState == 3)) {
            let componentPizzaTemplates = this._componentPizzaTemplates.filter(x => x.pizzaTemplateId == pizzaTemplate.id);
            pizzaTemplate.restaurantSetComponents = [];
            pizzaTemplate.finalComponents = [];
            for (const componentPizzaTemplate of componentPizzaTemplates) {
                let component = this._components.filter(x => x.id == componentPizzaTemplate.componentId)[0]
                pizzaTemplate.restaurantSetComponents.push(component);
                pizzaTemplate.finalComponents.push(component);
                
            }
            pizzaTemplate.userSetComponents = this._components.filter(x => !pizzaTemplate.restaurantSetComponents.includes(x))
        }
    }

    componentsTriggerMgn(pizzaTemplate: IPizzaTemplateWithChildren, component: IComponent) {
        let tempCheckArr = [...pizzaTemplate.finalComponents]
        if (! tempCheckArr.includes(component)) {
            tempCheckArr.push(component)
        } 
        else {
            tempCheckArr = tempCheckArr.filter(x => x !== component)
        }
        pizzaTemplate.currentModifications = (pizzaTemplate.restaurantSetComponents.filter(x => tempCheckArr.indexOf(x) === -1).length)
        pizzaTemplate.currentExtras = tempCheckArr.length - pizzaTemplate.restaurantSetComponents.length;
        let result = (pizzaTemplate.extras >= pizzaTemplate.currentExtras && 
            pizzaTemplate.modifications >= pizzaTemplate.currentModifications);
        if (result) {
            pizzaTemplate.finalComponents = [...tempCheckArr]
        } 
        else {
            pizzaTemplate.currentModifications = (pizzaTemplate.restaurantSetComponents.filter(x => pizzaTemplate.finalComponents.indexOf(x) === -1).length)
            pizzaTemplate.currentExtras = pizzaTemplate.finalComponents.length - pizzaTemplate.restaurantSetComponents.length;
        }
        pizzaTemplate.currentExtras = pizzaTemplate.currentExtras < 0 ? 0 : pizzaTemplate.currentExtras;
        return result;
    }

    categoryChanged(categoryId: string) {
        this._categoryId = categoryId;
        if (this._cart.restaurantId !== "") {
            if (this._categoryId !== undefined) {
                this._displayedPizzaTemplates = this._restaurantFilteredPizzaTemplates.filter(x => x.categoryId === this._categoryId);  
            } else {
                this._displayedPizzaTemplates = this._restaurantFilteredPizzaTemplates;
            }
        }
    }

    restaurantChanged(restaurantId: string) {
        this._viewState = 2;
        localStorage.setItem("msg", "");
        this._cart.restaurantId = restaurantId;
        if (this._categoryId !== "" || this._categoryId === undefined) {
            this._activeRestaurant = this._restaurants.find(x => x.id == restaurantId)!;
            
            let restaurantFoods = this._restaurantFoods.filter(x => x.restaurantId === this._activeRestaurant!.id)
            let pizzas: IPizzaWithRestaurants[] = [];
            for (const restaurantFood of restaurantFoods) {
                let pizza = this._pizzas.find(x => x.id === restaurantFood.pizzaId)!;
                pizza.activeRestaurantFood = restaurantFood;
                pizzas.push(pizza);
            }
            for (const pizzaTemplate of this._pizzaTemplates) {
                this.loadCategoryCount(pizzaTemplate);
                let pizzaChildren = pizzas.filter(x => x.pizzaTemplateId === pizzaTemplate.id);
                if (pizzaChildren.length !== 0) {
                    // remove children whose price has not been set
                    pizzaTemplate.pizzas = [...pizzaChildren];
                    this._restaurantFilteredPizzaTemplates.push(pizzaTemplate)
                }
            }
            this._displayedPizzaTemplates = [...this._restaurantFilteredPizzaTemplates];
        }
        
    }

    addPizzaToCart(pizzaTemplate: IPizzaTemplateWithChildren) {
        console.log(pizzaTemplate.selectedPizza.takenRestaurants.length);
        let pizzaGross = pizzaTemplate.selectedPizza.takenRestaurants.find(x => x.restaurantId === this._activeRestaurant?.id)!.gross;
        let totalGross = pizzaGross;
        
        let changes = "";
        let componentsGross = 0;

        if (pizzaTemplate.varietyState === 3) {
            let removedComponents = pizzaTemplate.restaurantSetComponents.filter(x => pizzaTemplate.finalComponents.indexOf(x) === -1)
            let addedComponents = pizzaTemplate.finalComponents.filter(x => pizzaTemplate.restaurantSetComponents.indexOf(x) === -1)
            for (const removedComponent of removedComponents) {
                changes += " - " + removedComponent.name + ": " + removedComponent.gross + "€;"
            };
            for (const addedComponent of addedComponents) {
                changes += " + " + addedComponent.name + ": " + addedComponent.gross + "€;"
            };
            for (const finalComponent of pizzaTemplate.finalComponents) {
                componentsGross += finalComponent.gross;
            };
            let componentGrossBaseline = 0;
            for (const restaurantSetComponent of pizzaTemplate.restaurantSetComponents) {
                componentGrossBaseline += restaurantSetComponent.gross
            }
            componentGrossBaseline = Math.round((componentGrossBaseline + Number.EPSILON) * 100) / 100;
            componentsGross = componentsGross - componentGrossBaseline < 0 ? 0 : componentsGross - componentGrossBaseline;
            totalGross = Math.round((pizzaGross + componentsGross + Number.EPSILON) * 100) / 100;
        }

        let cartMealFeId = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
        let cartMeal: ICartMeal = {
            id: cartMealFeId,
            cartId: "",
            pizzaId: pizzaTemplate.selectedPizza.id,
            name: pizzaTemplate.selectedPizza.name,
            pizzaGross: pizzaGross,
            changes: changes,
            componentsGross: componentsGross,
            totalGross: totalGross,
        }
        this._cartMeals.push(cartMeal)
    }

    pay() {
        var e = document.getElementById("PaymentMethod") as HTMLFormElement;
        this._cart.paymentMethod = e.options[e.selectedIndex].value;
        this.cartService.createCart({
            restaurantId: this._activeRestaurant!.id,
            asDelivery: this._cart.asDelivery,
            userLocationId: this._cart.asDelivery === true? this._cart.userLocationId : null,
            paymentMethod: this._cart.paymentMethod,
            firstName: this._cart.firstName,
            lastName: this._cart.lastName,
            phone: this._cart.phone,
            })
        .then(
            response => {
                this._alert = alertHandler(SOURCE.CART, response.statusCode, response.errorMessage);
                console.log("cart created: " + response.statusCode, response.errorMessage)
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.cartService.getCarts().then(
                        response => {
                            this._alert = alertHandler(SOURCE.CART, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                this._carts = response.data!

                                let cart = this._carts.slice(-1).pop()!;
                                for (const cartMeal of this._cartMeals) {
                                    this.cartMealService.createCartMeal({
                                        cartId: cart.id,
                                        pizzaId: cartMeal.pizzaId,
                                        name: cartMeal.name,
                                        pizzaGross: cartMeal.pizzaGross,
                                        changes: cartMeal.changes,
                                        componentsGross: cartMeal.componentsGross,
                                        totalGross: cartMeal.totalGross,
                                        })
                                    .then(
                                        async response => {
                                            this._alert = alertHandler(SOURCE.CARTMEAL, response.statusCode, response.errorMessage);
                                            console.log(response.statusCode)
                                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                                this._msg = "Thank you for your purchase!"
                                                localStorage.setItem("msg", this._msg);
                                                this.reload()
                                            }
                                        }
                                    );
                                }
                            }
                        }
                    );
                }
            }
        );
    }

    loadCategoryCount(pizzaTemplate: IPizzaTemplateWithChildren) {
        let category = this._categorys.find(x => x.id === pizzaTemplate.categoryId)!;
        category.countInRestaurant++;
    }

    triggerComponentMenu(pizzaTemplate: IPizzaTemplateWithChildren) {
        pizzaTemplate.showComponentMenu = !pizzaTemplate.showComponentMenu;
    }

    switchCreateNewLocation() {
        this._createNewLocation = !this._createNewLocation;
    }

    locationSubmit(event: Event) {
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
                    console.log(response.statusCode, response.data, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.userLocationService.getUserLocations().then(
                            response => {
                                this._alert = alertHandler(SOURCE.USERLOCATION, response.statusCode, response.errorMessage);
                                if (response.statusCode >= 200 && response.statusCode < 300) {
                                    this._userLocations = response.data!;
                                    this._createNewLocation = this._userLocations.length === 0;
                                };
                                console.log(response.statusCode, response.data, response.errorMessage);
                            }
                        );
                        this._createNewLocation = !this._createNewLocation;
                    }
                    
                }   
            );

        event.preventDefault();
    }

    removeCartMeal(cartMeal: ICartMeal) {
        this._cartMeals = this._cartMeals.filter(x => x.id !== cartMeal.id);
    }

    reload() {
        location.reload(); 
    }
}