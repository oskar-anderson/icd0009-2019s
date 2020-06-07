import { autoinject } from 'aurelia-framework';
import { HttpClient } from "aurelia-fetch-client";
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IItem, IItemCreate } from 'domain/IItem';


@autoinject
export class ItemService {
    constructor(private appState: AppState, private httpClient: HttpClient) {
        console.log('appState: ', appState)
        console.log('httpClient: ', httpClient)
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'https://pitsariina.azurewebsites.net/api/v1.0/Item';

    async getItems(): Promise<IFetchResponse<IItem[]>> {
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
                const data = (await response.json()) as IItem[];
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

    async getItem(id: string): Promise<IFetchResponse<IItem>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + id, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IItem;
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

    async createItem(item: IItemCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .post(this._baseUrl, JSON.stringify(item), {
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

    async updateItem(item: IItem): Promise<IFetchResponse<string>> {
        console.log(JSON.stringify(item));
        try {
            const response = await this.httpClient
                .put(this._baseUrl + '/' + item.id, JSON.stringify(item), {
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

    async deleteItem(id: string): Promise<IFetchResponse<string>> {
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