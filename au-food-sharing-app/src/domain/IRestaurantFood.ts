export interface IRestaurantFood {
    id: string;
    mealId: string;
    pizzaId: string;
    restaurantId: string;
    gross: number;
    since: string;
    until: string;
}

export interface IRestaurantFoodCreate {
    mealId: string;
    pizzaId: string;
    restaurantId: string;
    gross: number;
    since: string;
    until: string;
}