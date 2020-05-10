import Axios from 'axios';
import { IGpsSession } from '@/domain/IGpsSession';

export abstract class GpsSessionsApi {
    private static axios = Axios.create(
        {
            baseURL: "https://sportmap.akaver.com/api/v1.0/GpsSessions/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(): Promise<IGpsSession[]> {
        const url = "";
        try {
            const response = await this.axios.get<IGpsSession[]>(url);
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

    static async delete(id: string, jwt: string): Promise<void> {
        const url = "" + id;
        try {
            const response = await this.axios.delete<IGpsSession>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }
}
