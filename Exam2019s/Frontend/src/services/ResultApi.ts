import Axios from 'axios';
import { IResult } from '@/domain/IResult';

export abstract class ResultApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Result/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(): Promise<IResult[]> {
        const url = "";
        try {
            const response = await this.axios.get<IResult[]>(url);
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

    static async get(id: string): Promise<IResult | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<IResult>(url);
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
            const response = await this.axios.delete<IResult>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(result: IResult, jwt: string): Promise<void> {
        const url = "" + result.id;
        try {
            const response = await this.axios.put<IResult>(url, result, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('edit response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(result: IResult, jwt: string): Promise<void> {
        const url = "";
        try {
            const response = await this.axios.post<IResult>(url, {
                quizId: result.quizId,
                score: result.score,
                questionToPickedAnswer: result.questionToPickedAnswer,
                personName: result.personName,
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
