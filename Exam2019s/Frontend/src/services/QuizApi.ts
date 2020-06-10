import Axios from 'axios';
import { IQuiz } from '@/domain/IQuiz';

export abstract class QuizApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Quiz/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(): Promise<IQuiz[]> {
        const url = "";
        try {
            const response = await this.axios.get<IQuiz[]>(url);
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

    static async get(id: string): Promise<IQuiz | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<IQuiz>(url);
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
            const response = await this.axios.delete<IQuiz>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(quiz: IQuiz, jwt: string): Promise<void> {
        const url = "" + quiz.id;
        try {
            const response = await this.axios.put<IQuiz>(url, quiz, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('edit response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(quiz: IQuiz, jwt: string): Promise<void> {
        const url = "";
        console.log(quiz)
        try {
            const response = await this.axios.post<IQuiz>(url, {
                name: quiz.name,
                description: quiz.description,
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
