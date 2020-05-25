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


export class PizzaSize {
    name: string;
    size: number;
    constructor(name: string, size: number) {
        this.name = name;
        this.size = size;
    }
}

export let Sizes = [new PizzaSize('Väike', 1), new PizzaSize('Suur', 3)];
