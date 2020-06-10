import { ILoginDTO } from '../types/ILoginDTO';
import Axios from 'axios';
import { IRegisterDTO } from '@/types/IRegisterDTO';

interface ILoginResponse {
    token: string;
    status: string;
}
export abstract class AccountApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getJwt(loginDTO: ILoginDTO): Promise<string | null> {
        const url = "account/login";
        try {
            const response = await this.axios.post<ILoginResponse>(url, loginDTO);
            console.log('getJwt response', response);
            if (response.status === 200) {
                return response.data.token;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async register(registerDTO: IRegisterDTO): Promise<string | null> {
        const url = "account/register";
        try {
            const response = await this.axios.post<ILoginResponse>(url, registerDTO);
            console.log('register response', response);
            if (response.status === 200) {
                return response.data.token;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }
}
