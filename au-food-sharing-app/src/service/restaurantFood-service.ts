import { autoinject } from 'aurelia-framework';
import { HttpClient } from "aurelia-fetch-client";
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IRestaurantFood, IRestaurantFoodCreate } from 'domain/IRestaurantFood';


@autoinject
export class RestaurantFoodService {
    constructor(private appState: AppState, private httpClient: HttpClient) {
        console.log('appState: ', appState)
        console.log('httpClient: ', httpClient)
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'https://pitsariina.azurewebsites.net/api/v1.0/RestaurantFood';

    async getRestaurantFoods(): Promise<IFetchResponse<IRestaurantFood[]>> {
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
                const data = (await response.json()) as IRestaurantFood[];
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

    async getRestaurantFood(id: string): Promise<IFetchResponse<IRestaurantFood>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + id, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IRestaurantFood;
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

    async createRestaurantFood(restaurantFood: IRestaurantFoodCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .post(this._baseUrl, JSON.stringify(restaurantFood), {
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

    async updateRestaurantFood(restaurantFood: IRestaurantFood): Promise<IFetchResponse<string>> {
        console.log(JSON.stringify(restaurantFood));
        try {
            const response = await this.httpClient
                .put(this._baseUrl + '/' + restaurantFood.id, JSON.stringify(restaurantFood), {
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

    async deleteRestaurantFood(id: string): Promise<IFetchResponse<string>> {
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