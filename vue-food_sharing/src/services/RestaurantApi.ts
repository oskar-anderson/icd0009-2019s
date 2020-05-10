import Axios from 'axios';
import { IRestaurant } from '@/domain/IRestaurant';

export abstract class RestaurantApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Restaurant/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(): Promise<IRestaurant[]> {
        const url = "";
        try {
            const response = await this.axios.get<IRestaurant[]>(url);
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

    static async get(id: string): Promise<IRestaurant | null> {
        console.log("id: ", id);
        const url = "" + id;
        try {
            const response = await this.axios.get<IRestaurant>(url);
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
            const response = await this.axios.delete<IRestaurant>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(restaurant: IRestaurant, jwt: string): Promise<void> {
        const url = "" + restaurant.id;
        try {
            const response = await this.axios.put<IRestaurant>(url, restaurant, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('edit response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(restaurant: IRestaurant, jwt: string): Promise<void> {
        const url = "";
        try {
            const response = await this.axios.post<IRestaurant>(url, {
                name: restaurant.name,
                location: restaurant.location,
                telephone: restaurant.telephone,
                openTime: restaurant.openTime,
                openNotification: restaurant.openNotification
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
