import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { HttpClient, json } from 'aurelia-fetch-client';
import { IFetchResponse } from 'types/IFetchResponse';
import { ILoginResponse } from 'domain/ILoginResponse';
import { IRegisterResponse } from 'domain/IRegisterResponse';


@autoinject
export class AccountService {
    constructor(
        private appState: AppState,
        private httpClient: HttpClient) 
        {
            this.httpClient.baseUrl = this.appState.baseUrl;
        }

    async login(email: string, password: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/login', JSON.stringify({
                email: email,
                password: password,
            }), {
                cache: 'no-store'
            });

            // happy case
            if (response.status >= 200 && response.status < 300){
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            } 

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }
    
    async register(firstName: string, lastName: string, phone: string, email: string, password: string): Promise<IFetchResponse<IRegisterResponse>> {
        try {
            const response = await this.httpClient.post('account/register', JSON.stringify({
                firstName: firstName,
                lastName: lastName,
                phone:  phone,
                email: email,
                password: password,
            }), {
                cache: 'no-store'
            });
            
            // happy case
            if (response.status >= 200 && response.status < 300){
                const data = (await response.json()) as IRegisterResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            } 
  
            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }
}
