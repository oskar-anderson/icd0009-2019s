import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountManageEmail {

    private _email: string = "";
    private _newEmail: string = ""
    private _errorMessage: string = "";
    private _successMessage: string = "";
    private _activeNr = 2;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router) {

    }

    attached() {
        this._email = this.appState.email as string;
    }

    onSubmit(event: Event) {
        event.preventDefault();

        this.accountService.changeEmail(this._email, this._newEmail).then(
            response => {
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.appState.email = this._newEmail;
                    this._newEmail = "";
                    this._successMessage = "Much success!!!"
                    this.attached();
                } else {
                    this._errorMessage = response.statusCode.toString() + ' ' + response.errorMessage!
                }
            }
        );
    }

}
