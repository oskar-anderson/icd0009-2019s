import Axios from 'axios';
import { IPizzaTemplate } from '@/domain/IPizzaTemplate';

export abstract class PizzaTemplateApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/PizzaTemplate/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(): Promise<IPizzaTemplate[]> {
        const url = "";
        try {
            const response = await this.axios.get<IPizzaTemplate[]>(url);
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

    static async get(id: string): Promise<IPizzaTemplate | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<IPizzaTemplate>(url);
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
            const response = await this.axios.delete<IPizzaTemplate>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(pizzaTemplate: IPizzaTemplate, jwt: string): Promise<void> {
        const url = "" + pizzaTemplate.id;
        try {
            const response = await this.axios.put<IPizzaTemplate>(url, pizzaTemplate, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('edit response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(pizzaTemplate: IPizzaTemplate, jwt: string): Promise<void> {
        const url = "";
        try {
            const response = await this.axios.post<IPizzaTemplate>(url, {
                categoryId: pizzaTemplate.categoryId,
                name: pizzaTemplate.name,
                picture: pizzaTemplate.picture,
                modification: pizzaTemplate.modifications,
                extras: pizzaTemplate.extras,
                description: pizzaTemplate.description,
                varietyState: pizzaTemplate.varietyState,
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
