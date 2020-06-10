import { ICartMeal } from './ICartMeal';
import { IUserLocation } from './IUserLocation';

export interface ICart {
    id: string;
    restaurantId: string;
    asDelivery: boolean;
    userLocationId: string | null;
    paymentMethod: string;
    firstName: string;
    lastName: string;
    phone: string;
}

export interface ICartCreate {
    restaurantId: string;
    asDelivery: boolean;
    userLocationId: string | null;
    paymentMethod: string;
    firstName: string;
    lastName: string;
    phone: string;
}

export interface ICartIndex {
    id: string;
    restaurantId: string;
    asDelivery: boolean;
    userLocationId: string | null;
    paymentMethod: string;
    firstName: string;
    lastName: string;
    phone: string;

    restaurantName: string,
    userLocation: IUserLocation,
    cartMeals: ICartMeal[];
    total: number;
    address: string;
    sharingName: string;
}