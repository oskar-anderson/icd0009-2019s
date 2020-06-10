import { IPizza } from '../domain/IPizza'

export interface IRestaurantFood {
    id: string;
    pizzaId: string;
    restaurantId: string;
    gross: number;
}

export interface IRestaurantFoodCreate {
    pizzaId: string;
    restaurantId: string;
    gross: number;
}