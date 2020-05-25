import { IComponent } from '../domain/IComponent'

export interface IPizzaTemplate {
    id: string;
    categoryId: string;
    name: string;
    picture: string;
    modifications: number;
    extras: number;
    description: string;    
}

export interface IPizzaTemplateCreate {
    categoryId: string;
    category: null;
    name: string;
    picture: string;
    modifications: number;
    extras: number;
    description: string;

}

export interface IPizzaTemplateWithChildren {
    id: string;
    categoryId: string;
    pizzaNames: string[];
    takenComponents: IComponent[];
    freeComponents: IComponent[];
    chosenComponentId: string,
    name: string;
    picture: string;
    modifications: number;
    extras: number;
    description: string;    
}

export class IPizzaTemplateIndex {
    constructor(id: string, pizzaName: string){

    };
}
