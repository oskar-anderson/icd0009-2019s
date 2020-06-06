import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IOwner } from 'domain/IOwner';

@autoinject
export class OwnerService {
    constructor(private httpClient: HttpClient) {

    }

    private readonly _baseUrl = 'https://localhost:5001/api/Owners';

    getOwners(): Promise<IOwner[]> {
        return this.httpClient
            .fetch(this._baseUrl, { cache: "no-store" })
            .then(response => response.json())
            .then((data: IOwner[]) => data)
            .catch(reason => {
                console.error(reason);
                return [];
            });

    }

    getOwner(id: string): Promise<IOwner | null> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id, { cache: "no-store" })
            .then(response => response.json())
            .then((data: IOwner) => data)
            .catch(reason => {
                console.error(reason);
                return null;
            });
    }

    createOwner(owner: IOwner): Promise<string> {
        return this.httpClient.post(this._baseUrl, JSON.stringify(owner), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('createOwner response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

    updateOwner(owner: IOwner): Promise<string> {
        return this.httpClient.put(this._baseUrl + '/' + owner.id, JSON.stringify(owner), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('updateOwner response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

    deleteOwner(owner: IOwner): Promise<string> {
        return this.httpClient.delete(this._baseUrl + '/' + owner.id, JSON.stringify(owner), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('deleteOwner response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

}
