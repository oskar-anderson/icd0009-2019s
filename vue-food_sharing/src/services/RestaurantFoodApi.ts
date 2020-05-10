import Axios from 'axios';
import { IRestaurantFood } from '@/domain/IRestaurantFood';

export abstract class RestaurantFoodApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/RestaurantFood/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(): Promise<IRestaurantFood[]> {
        const url = "";
        try {
            const response = await this.axios.get<IRestaurantFood[]>(url);
            console.log('getAll response', response);
            if (response.status === 200) {
                return response.data;
            }
            return [];
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return [];
        }
    }

    static async get(id: string): Promise<IRestaurantFood | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<IRestaurantFood>(url);
            console.log('get response', response);
            if (response.status === 200) {
                return response.data;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async delete(id: string, jwt: string): Promise<void> {
        const url = "" + id;
        console.log(jwt);
        try {
            const response = await this.axios.delete<IRestaurantFood>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(restaurantFood: IRestaurantFood, jwt: string): Promise<void> {
        const url = "" + restaurantFood.id;
        try {
            const response = await this.axios.put<IRestaurantFood>(url, restaurantFood, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('edit response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(restaurantFood: IRestaurantFood, jwt: string): Promise<void> {
        const url = "";
        try {
            const response = await this.axios.post<IRestaurantFood>(url, {
                mealId: restaurantFood.mealId,
                pizzaId: null,
                restaurantId: restaurantFood.restaurantId,
                name: restaurantFood.name,
                tax: restaurantFood.tax,
                gross: restaurantFood.gross,
                since: restaurantFood.since,
                until: restaurantFood.until,
            }, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('create response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }
}
