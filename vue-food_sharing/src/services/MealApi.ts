import Axios from 'axios';
import { IMeal } from '@/domain/IMeal';

export abstract class MealApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Meal/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(): Promise<IMeal[]> {
        const url = "";
        try {
            const response = await this.axios.get<IMeal[]>(url);
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

    static async get(id: string): Promise<IMeal | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<IMeal>(url);
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
            const response = await this.axios.delete<IMeal>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(meal: IMeal, jwt: string): Promise<void> {
        const url = "" + meal.id;
        try {
            const response = await this.axios.put<IMeal>(url, meal, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('edit response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(meal: IMeal, jwt: string): Promise<void> {
        const url = "";
        try {
            const response = await this.axios.post<IMeal>(url, {
                categoryId: meal.categoryId,
                name: meal.name,
                picture: meal.picture,
                description: meal.description,
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
