import { IComponent } from '../domain/IComponent'
import { IPizzaWithRestaurants, PizzaSize } from './IPizza';

export interface IPizzaTemplate {
    id: string;
    categoryId: string;
    name: string;
    picture: string;
    modifications: number;
    extras: number;
    description: string; 
    varietyState: number   
}

export interface IPizzaTemplateCreate {
    categoryId: string;
    category: null;
    name: string;
    picture: string;
    modifications: number;
    extras: number;
    description: string;
    varietyState: number

}

export interface IPizzaTemplateWithChildren {
    id: string;
    categoryId: string;
    pizzaNames: string[];
    pizzas: IPizzaWithRestaurants[];
    takenComponents: IComponent[];
    freeComponents: IComponent[];
    chosenComponentId: string;
    name: string;
    picture: string;
    modifications: number;
    extras: number;
    description: string;
    varietyState: number;

    newPizzaName: string;
    newPizzaSizeChoice: number;
    freeSizes: PizzaSize[];
    showComponentMenu: boolean;
    restaurantSetComponents: IComponent[];
    userSetComponents: IComponent[];
    finalComponents: IComponent[];
    componentsAreLoaded: boolean;
    currentExtras: number;
    currentModifications: number;
    selectedPizza: IPizzaWithRestaurants;

}

