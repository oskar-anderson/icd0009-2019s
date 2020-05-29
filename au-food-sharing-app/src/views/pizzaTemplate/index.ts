import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

import { IPizzaTemplate, IPizzaTemplateWithChildren } from './../../domain/IPizzaTemplate';
import { PizzaTemplateService } from 'service/pizzaTemplate-service';

import { IPizza, IPizzaWithRestaurants, Sizes } from './../../domain/IPizza';
import { PizzaService } from 'service/pizza-service';

import { ComponentService } from 'service/component-service';
import { IComponent } from './../../domain/IComponent';

import { RestaurantFoodService } from 'service/restaurantFood-service';
import { IRestaurantFood } from './../../domain/IRestaurantFood';

import { RestaurantService } from 'service/restaurant-service';
import { IRestaurant } from './../../domain/IRestaurant';

import { ComponentPizzaTemplateService } from 'service/componentPizzaTemplate-service';
import { IComponentPizzaTemplate } from './../../domain/IComponentPizzaTemplate';


import { stringify } from 'querystring';
import { TextHandler } from 'aurelia-loader-nodejs';


@autoinject
export class PizzaTemplateIndex{
    private _pizzas: IPizzaWithRestaurants[] = [];
    private _pizzaRestaurants: IPizzaWithRestaurants[] = [];

    private _components: IComponent[] = [];
    private _componentPizzaTemplates: IComponentPizzaTemplate[] = [];

    private _restaurantFoods: IRestaurantFood[] = [];
    private _restaurants: IRestaurant[] = [];
    
    private _pizzaTemplates: IPizzaTemplateWithChildren[] = [];

    // view settings
    private _showComponentAssignment: boolean = false;
    private _showTechnical: boolean = false;
    private _showPictures: boolean = false;
    private _showOptionAssignment: boolean = false;


    // pizza settings
    private _sizes = Sizes;
    
    private _alert: IAlertData | null = null;

    constructor(
        private pizzaTemplateService: PizzaTemplateService, 
        private pizzaService: PizzaService, 
        private componentService: ComponentService,
        private componentPizzaTemplateService: ComponentPizzaTemplateService,
        private restaurantFoodService: RestaurantFoodService,
        private restaurantService: RestaurantService,
        ){

    }
    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    
    }
    attached() {
        console.log(11)
        this.pizzaTemplateService.getPizzaTemplates().then(
            response => {
                this._alert = alertHandler(SOURCE.PIZZATEMPLATE, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log(111)
                    this._pizzaTemplates = response.data!

                    console.log(22)
                    this.pizzaService.getPizzas().then(
                        response => {
                            this._alert = alertHandler(SOURCE.PIZZA, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                console.log(222)
                                this._pizzas = response.data!;
                                console.log("calling setNames")
                                this.getChildren();


                                this.restaurantFoodService.getRestaurantFoods().then(
                                    response => {
                                        this._alert = alertHandler(SOURCE.RESTAURANTFOOD, response.statusCode, response.errorMessage);
                                        if (response.statusCode >= 200 && response.statusCode < 300) {
                                            this._restaurantFoods = response.data!;
                                            this.getRestaurantFoodsToPizza();
                        
                                            this.restaurantService.getRestaurants().then(
                                                response => {
                                                    this._alert = alertHandler(SOURCE.RESTAURANT, response.statusCode, response.errorMessage);
                                                    if (response.statusCode >= 200 && response.statusCode < 300) {
                                                        this._restaurants = response.data!;
                                                    }
                                                }
                                            )
                                        }
                                    }
                                );
                            }
                        }
                    )
                }
            }
        )
        console.log(33)
        this.componentService.getComponents().then(
            response => {
                this._alert = alertHandler(SOURCE.COMPONENT, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log(333)
                    this._components = response.data!;

                    console.log(44)
                    this.componentPizzaTemplateService.getComponentPizzaTemplates().then(
                        response => {
                            this._alert = alertHandler(SOURCE.COMPONENTPIZZATPL, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                console.log(444)
                                this._componentPizzaTemplates = response.data!;
                                console.log("calling setComponents")
                                this.getComponents();
                            }
                        }
                    );
                }
            }
        );
    }


    getChildren() {
        console.log("in getNames")
        for (let pizzaTemplate of this._pizzaTemplates) {
            let children = this._pizzas.filter(x => x.pizzaTemplateId == pizzaTemplate.id);
            pizzaTemplate.pizzaNames = [];
            pizzaTemplate.pizzas = [];
            pizzaTemplate.freeSizes = Sizes;
            children.forEach(child => {
                pizzaTemplate.pizzas.push(child);
                let pizzaName = "";
                pizzaName += child.name;
                pizzaName += children.length - 1 == children.indexOf(child) ? "" : ", ";
                pizzaTemplate.pizzaNames.push(pizzaName);
                pizzaTemplate.freeSizes = pizzaTemplate.freeSizes.filter(x => x.size != child.sizeNumber)
            });
            pizzaTemplate.freeSizes = pizzaTemplate.freeSizes.find(x => x.valueName === "") === undefined ? pizzaTemplate.freeSizes : [];
            console.log(pizzaTemplate.pizzaNames.length)
            console.log(pizzaTemplate.pizzas.length)
        };
    }

    getComponents() {
        console.log("in getComponents")
        for (let pizzaTemplate of this._pizzaTemplates.filter(x => x.varietyState == 3)) {
            let componentPizzaTemplates = this._componentPizzaTemplates.filter(x => x.pizzaTemplateId == pizzaTemplate.id);
            pizzaTemplate.takenComponents = [];
            for (const componentPizzaTemplate of componentPizzaTemplates) {
                let component = this._components.filter(x => x.id == componentPizzaTemplate.componentId)[0]
                pizzaTemplate.takenComponents.push(component);
            }
            pizzaTemplate.freeComponents = this._components.filter(x => pizzaTemplate.takenComponents.indexOf(x) === -1)
            console.log(pizzaTemplate.takenComponents.length)
        }
    }

    getRestaurantFoodsToPizza() {
        for (let pizzaTemplate of this._pizzaTemplates) {
            for (const pizza of pizzaTemplate.pizzas) {
                let restaurantFoods = this._restaurantFoods.filter(x => x.pizzaId == pizza.id);
                pizza.takenRestaurants = [...restaurantFoods];
                pizza.freeRestaurants = this._restaurantFoods.filter(x => pizza.takenRestaurants.indexOf(x) === -1)    
            }
        }
    }

    TriggerComponentAssignment(){
        this._showComponentAssignment = !this._showComponentAssignment;
    }
    TriggerTechnical(){
        this._showTechnical = !this._showTechnical;
    }
    TriggerPictures(){
        this._showPictures = !this._showPictures;
    }
    TriggerOptionAssignment(){
        this._showOptionAssignment = !this._showOptionAssignment;
    }
    

    AddComponent(event: Event, pizzaTemplateId: string, componentId: string) {
        this.componentPizzaTemplateService
            .createComponentPizzaTemplate({
                componentId: componentId,
                pizzaTemplateId: pizzaTemplateId,
                })
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.COMPONENTPIZZATPL, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        location.reload()
                    }
                }   
            );

        event.preventDefault();
    }

    RemoveComponent(event: Event, pizzaTemplateId: string, componentId: string) {
        let removeMe = this._componentPizzaTemplates.filter(x => x.componentId == componentId && x.pizzaTemplateId == pizzaTemplateId)[0];
        this.componentPizzaTemplateService
            .deleteComponentPizzaTemplate(removeMe.id)
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.COMPONENTPIZZATPL, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        location.reload()
                    }
                }   
            );

        event.preventDefault();
    }

    DropdownChanged(pizzaTemplate: IPizzaTemplateWithChildren) {
        let size = this._sizes[parseInt(pizzaTemplate.newPizzaSizeChoice + "")].displayName;
        pizzaTemplate.newPizzaName = size === "" ? pizzaTemplate.name : pizzaTemplate.name + " - " + size;
    }

    onSubmit(event: Event, pizzaTemplate: IPizzaTemplateWithChildren) {
        this.pizzaService
            .createPizza({
                pizzaTemplateId: pizzaTemplate.id,
                pizzaTemplate: null,
                sizeNumber: this._sizes[parseInt(pizzaTemplate.newPizzaSizeChoice + "")].size,
                sizeName: this._sizes[parseInt(pizzaTemplate.newPizzaSizeChoice + "")].valueName,
                name: pizzaTemplate.newPizzaName,
                })
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.PIZZA, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        location.reload()
                    }
                }   
            );

        event.preventDefault();
    }

    getVarietyStateName(i: number) {
        if (i === 2) { return "V"; }
        if (i === 3) { return "C"; }
        return "Validation has failed";
    }
}