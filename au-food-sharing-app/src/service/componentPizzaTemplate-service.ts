import { autoinject } from 'aurelia-framework';
import { HttpClient } from "aurelia-fetch-client";
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IComponentPizzaTemplate, IComponentPizzaTemplateCreate  } from 'domain/IComponentPizzaTemplate';


@autoinject
export class ComponentPizzaTemplateService {
    constructor(private appState: AppState, private httpClient: HttpClient) {
        console.log('appState: ', appState)
        console.log('httpClient: ', httpClient)
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'https://pitsariina.azurewebsites.net/api/v1.0/ComponentPizzaTemplate';

    async getComponentPizzaTemplates(): Promise<IFetchResponse<IComponentPizzaTemplate[]>> {
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
                const data = (await response.json()) as IComponentPizzaTemplate[];
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

    async getComponentPizzaTemplate(id: string): Promise<IFetchResponse<IComponentPizzaTemplate>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + id, {
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IComponentPizzaTemplate;
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

    async createComponentPizzaTemplate(componentPizzaTemplate: IComponentPizzaTemplateCreate): Promise<IFetchResponse<string>> {
        try {
            console.log(JSON.stringify(componentPizzaTemplate))
            const response = await this.httpClient
                .post(this._baseUrl, JSON.stringify(componentPizzaTemplate), {
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

    async updateComponentPizzaTemplate(componentPizzaTemplate: IComponentPizzaTemplate): Promise<IFetchResponse<string>> {
        console.log(JSON.stringify(componentPizzaTemplate));
        try {
            const response = await this.httpClient
                .put(this._baseUrl + '/' + componentPizzaTemplate.id, JSON.stringify(componentPizzaTemplate), {
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

    async deleteComponentPizzaTemplate(id: string): Promise<IFetchResponse<string>> {
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