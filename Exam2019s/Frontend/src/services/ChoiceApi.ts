import Axios from 'axios';
import { IChoice } from '@/domain/IChoice';

export abstract class ChoiceApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Choice/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(): Promise<IChoice[]> {
        const url = "";
        try {
            const response = await this.axios.get<IChoice[]>(url);
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

    static async get(id: string): Promise<IChoice | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<IChoice>(url);
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
        try {
            const response = await this.axios.delete<IChoice>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(choice: IChoice, jwt: string): Promise<void> {
        const url = "" + choice.id;
        try {
            const response = await this.axios.put<IChoice>(url, choice, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('edit response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(choice: IChoice, jwt: string): Promise<void> {
        const url = "";
        try {
            const response = await this.axios.post<IChoice>(url, {
                name: choice.name,
                givesPoints: choice.givesPoints,
                questionId: choice.questionId
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
