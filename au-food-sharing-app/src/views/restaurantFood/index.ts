import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

import { IPizzaTemplate, IPizzaTemplateWithChildren } from './../../domain/IPizzaTemplate';
import { PizzaTemplateService } from 'service/pizzaTemplate-service';

import { IPizza } from './../../domain/IPizza';
import { PizzaService } from 'service/pizza-service';

import { ComponentService } from 'service/component-service';
import { IComponent } from './../../domain/IComponent';

import { ComponentPizzaTemplateService } from 'service/componentPizzaTemplate-service';
import { IComponentPizzaTemplate } from './../../domain/IComponentPizzaTemplate';


import { stringify } from 'querystring';
import { TextHandler } from 'aurelia-loader-nodejs';


@autoinject
export class PizzaTemplateIndex{
    private _pizzas: IPizza[] = [];
    private _components: IComponent[] = [];
    private _componentPizzaTemplates: IComponentPizzaTemplate[] = [];
    private _pizzaTemplates: IPizzaTemplateWithChildren[] = [];
    private _showComponentSet: boolean = false;

    private _alert: IAlertData | null = null;

    constructor(private pizzaTemplateService: PizzaTemplateService, 
        private pizzaService: PizzaService, 
        private componentService: ComponentService,
        private componentPizzaTemplateService: ComponentPizzaTemplateService){

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
                    this._pizzaTemplates = response.data!;

                    console.log(22)
                    this.pizzaService.getPizzas().then(
                        response => {
                            this._alert = alertHandler(SOURCE.PIZZA, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                console.log(222)
                                this._pizzas = response.data!;
                                console.log("calling setNames")
                                this.getChildren();
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
            children.forEach(child => {
                let pizzaName = "";
                pizzaName += child.name;
                pizzaName += children.length - 1 == children.indexOf(child) ? "" : ", ";
                pizzaTemplate.pizzaNames.push(pizzaName);
            });
            console.log(pizzaTemplate.pizzaNames.length)
        };
    }

    getComponents() {
        console.log("in getComponents")
        for (let pizzaTemplate of this._pizzaTemplates) {
            let links = this._componentPizzaTemplates.filter(x => x.pizzaTemplateId == pizzaTemplate.id);
            pizzaTemplate.takenComponents = [];
            for (const link of links) {
                let component = this._components.filter(x => x.id == link.componentId)[0]
                pizzaTemplate.takenComponents.push(component);
            }
            pizzaTemplate.freeComponents = this._components.filter(x => pizzaTemplate.takenComponents.indexOf(x) === -1)
            console.log(pizzaTemplate.takenComponents.length)
        }
    }

    show(){
        this._showComponentSet = !this._showComponentSet;
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
}