import { ILoginDTO } from './../types/ILoginDTO';
import Axios from 'axios';

interface ILoginResponse {
    token: string;
    status: string;
}
export abstract class AccountApi {
    private static axios = Axios.create(
        {
            baseURL: "https://sportmap.akaver.com/api/v1.0/",
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
}
