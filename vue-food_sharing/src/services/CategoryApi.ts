import Axios from 'axios';
import { ICategory } from '@/domain/ICategory';

export abstract class CategoryApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Category/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(): Promise<ICategory[]> {
        const url = "";
        try {
            const response = await this.axios.get<ICategory[]>(url);
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

    static async get(id: string): Promise<ICategory | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<ICategory>(url);
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
            const response = await this.axios.delete<ICategory>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(category: ICategory, jwt: string): Promise<void> {
        const url = "" + category.id;
        try {
            const response = await this.axios.put<ICategory>(url, category, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('edit response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(category: ICategory, jwt: string): Promise<void> {
        const url = "";
        try {
            const response = await this.axios.post<ICategory>(url, {
                name: category.name,
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
