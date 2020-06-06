import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountManage {
    
    private _email: string= "";
    private _phoneNumber: string= "";
    private _errorMessage: string = "";
    private _successMessage: string = "";
    private _activeNr = 1;

    constructor(private accountService: AccountService, private appState: AppState){
        
    }

    attached(){
        this._email = this.appState.email as string;
        this._phoneNumber = this.appState.phoneNumber as string;
    }

}
