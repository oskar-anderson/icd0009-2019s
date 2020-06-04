import { IRestaurantFood } from '../domain/IRestaurantFood'

export interface IPizza {
    id: string;
    pizzaTemplateId: string;
    sizeNumber: number;
    sizeName: string;
    name: string;
}

export interface IPizzaCreate {
    pizzaTemplateId: string;
    pizzaTemplate: null;
    sizeNumber: number;
    sizeName: string;
    name: string;
}


export interface IPizzaWithRestaurants {
    id: string;
    pizzaTemplateId: string;
    pizzaTemplate: null;
    sizeNumber: number;
    sizeName: string;
    name: string;
    freeRestaurants: IRestaurantFood[];
    takenRestaurants: IRestaurantFood[];
    activeRestaurantFood: IRestaurantFood;
    isSelectedPizza: boolean;
}

export class PizzaSize {
    displayName: string;
    size: number;
    valueName: string;
    constructor(displayName: string, size: number, valueName: string) {
        this.displayName = displayName;
        this.size = size;
        this.valueName = valueName;
    }
}

export let Sizes = [new PizzaSize('Suurus puudub', 2, ''), new PizzaSize('Väike', 1, 'Väike'), new PizzaSize('Suur', 3, 'Suur')];
