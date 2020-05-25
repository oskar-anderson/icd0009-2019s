import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { ICart } from 'domain/ICart';
import { AppState } from 'state/app-state'

@autoinject
export class CartService{
    constructor(private appState: AppState, private httpClient: HttpClient) {

    }

    private readonly _baseUrl = 'https://localhost:5001/api/v1.0/Cart';

    getCarts(): Promise<ICart[]> {
        let x: RequestInit;

        return this.httpClient
        .fetch(this._baseUrl, { 
            cache: "no-store", headers: {
                authorization: "Bearer " + this.appState.jwt
            }
        })
        .then(response => response.json())
        .then((data: ICart[]) => data)
        .catch(reason => {
            console.error(reason);
            return []; 
        });
    }
    getCart(id: string) {

    }

}