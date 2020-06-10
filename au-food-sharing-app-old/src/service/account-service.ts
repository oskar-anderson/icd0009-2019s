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

    async changeNames(email: string, firstName: string, lastName: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/changeNames', JSON.stringify({
                email: email,
                firstName: firstName,
                lastName: lastName,
            }), {
                cache: 'no-store'
            });

            // Everything went well!
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // Something went wrong!!
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


    async changePassword(email: string, oldPassword: string, newPassword: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/changePassword', JSON.stringify({
                email: email,
                oldPassword: oldPassword,
                newPassword: newPassword,
            }), {
                cache: 'no-store'
            });

            // Everything went well!
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // Something went wrong!!
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


    async changeEmail(email: string, newEmail: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/changeEmail', JSON.stringify({
                email: email,
                newEmail: newEmail,
            }), {
                cache: 'no-store'
            });

            // Everything went well!
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // Something went wrong!!
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

    async changePhoneNumber(email: string, phoneNumber: string, newPhoneNumber: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/changePhoneNumber', JSON.stringify({
                email: email,
                phoneNumber: phoneNumber,
                newPhoneNumber: newPhoneNumber,
            }), {
                cache: 'no-store'
            });

            // Everything went well!
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // Something went wrong!!
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
