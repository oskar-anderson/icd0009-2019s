import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountManagePassword {
    
    private _email: string = ""
    private _oldPassword: string = "";
    private _newPassword: string = "";
    private _confirmPassword: string = "";
    private _errorMessage: string = "";
    private _successMessage: string = "";
    private _activeNr = 3;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router){
        
    }

    attached() {
        this._oldPassword = this.appState.password as string;
        this._email = this.appState.email as string;
    }

    onSubmit(event: Event){
        event.preventDefault();

        if (this._oldPassword === this._newPassword) {
            this._errorMessage = "New password needs to differ from current password!"
        }
        else if (this._newPassword !== this._confirmPassword) {
            this._errorMessage = "Passwords need to match!"
        }
        if (this._errorMessage !== "") {
            return null;
        }

        this.accountService.changePassword(this._email, this._oldPassword, this._newPassword).then(
            response => {
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.appState.password = this._newPassword;
                    this._newPassword = "";
                    this._confirmPassword = "";
                    this._successMessage = "Much success!!!"
                    this.attached();
                } else {
                    this._errorMessage = response.statusCode.toString() + ' ' + response.errorMessage!
                }
            }
        );
    }
    
}
