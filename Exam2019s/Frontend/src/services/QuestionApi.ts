import Axios from 'axios';
import { IQuestion } from '@/domain/IQuestion';

export abstract class QuestionApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Question/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(): Promise<IQuestion[]> {
        const url = "";
        try {
            const response = await this.axios.get<IQuestion[]>(url);
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

    static async get(id: string): Promise<IQuestion | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<IQuestion>(url);
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
            const response = await this.axios.delete<IQuestion>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(question: IQuestion, jwt: string): Promise<void> {
        const url = "" + question.id;
        try {
            const response = await this.axios.put<IQuestion>(url, question, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('edit response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(question: IQuestion, jwt: string): Promise<void> {
        const url = "";
        console.log(question);
        try {
            const response = await this.axios.post<IQuestion>(url, {
                orderNumber: parseInt(question.orderNumber + ""),
                questionName: question.questionName,
                points: parseInt(question.points + ""),
                quizId: question.quizId,
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
