import { autoinject } from 'aurelia-framework';
import { HttpClient } from "aurelia-fetch-client";
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IUserLocation, IUserLocationCreate } from 'domain/IUserLocation';


@autoinject
export class UserLocationService {
    constructor(private appState: AppState, private httpClient: HttpClient) {
        console.log('appState: ', appState)
        console.log('httpClient: ', httpClient)
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'https://pitsariina.azurewebsites.net/api/v1.0/UserLocation';

    async getUserLocations(): Promise<IFetchResponse<IUserLocation[]>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IUserLocation[];
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

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getUserLocation(id: string): Promise<IFetchResponse<IUserLocation>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + id, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IUserLocation;
                return {
                    statusCode: response.status,
                    data: data
                }
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async createUserLocation(userLocation: IUserLocationCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .post(this._baseUrl, JSON.stringify(userLocation), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                })

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }

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

    async updateUserLocation(userLocation: IUserLocation): Promise<IFetchResponse<string>> {
        console.log(JSON.stringify(userLocation));
        try {
            const response = await this.httpClient
                .put(this._baseUrl + '/' + userLocation.id, JSON.stringify(userLocation), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }
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

    async deleteUserLocation(id: string): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
            .delete(this._baseUrl + '/' + id, null, {
                cache: 'no-store',
                headers: {
                    authorization: "Bearer " + this.appState.jwt
                }
            });

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }
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