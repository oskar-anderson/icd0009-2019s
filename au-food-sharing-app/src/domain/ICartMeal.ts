export interface ICartMeal {
    id: string;
    cartId: string;
    pizzaId: string;
    name: string;
    pizzaGross: number;
    changes: string;
    componentsGross: number;
    totalGross: number;
}

export interface ICartMealCreate {
    cartId: string;
    pizzaId: string;
    name: string;
    pizzaGross: number;
    changes: string;
    componentsGross: number;
    totalGross: number;
}