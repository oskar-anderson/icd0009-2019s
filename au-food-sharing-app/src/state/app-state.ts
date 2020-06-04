import * as JwtDecode from "jwt-decode";

export class AppState {
    constructor(){
    }

    public readonly baseUrl = 'https://localhost:5001/api/v1.0/';

    // JavaScript Object Notation Web Token 
    // to keep track of logged in status
    // https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage
    get jwt():string | null {
        return localStorage.getItem('jwt');
    }

    set jwt(value: string | null){
        if (value){
            localStorage.setItem('jwt', value);
        } else {
            localStorage.removeItem('jwt');
        }
    }

    get isAdmin(): boolean {
        if (this.jwt) {
            const decoded = JwtDecode(this.jwt) as Record<string, string>;
            let userRole = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            if (userRole.includes('Admin')) {
                userRole = 'Admin';
                return true;
            }
            return false;
        }
        return false;
    }

}
