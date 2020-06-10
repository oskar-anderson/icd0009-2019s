import { Router } from 'aurelia-router';
import { AppState } from './../../state/app-state';
import { AccountService } from './../../service/account-service';
import { autoinject } from 'aurelia-framework';

@autoinject
export class AccountRegister {
    
    private _firstName : string = "";
    private _lastName : string = "";
    private _phone : string = "";
    private _email : string = "";
    private _password : string = "";
    private _condfirmPassword : string = "";
    private _errorMessage : string | null = null;

    constructor(private accountService : AccountService, private appState : AppState, private router : Router){
    
    }

    onSubmit(event: Event) {
  
      console.log(this._email, this._password);
      event.preventDefault();

      this.accountService.register(this._firstName, this._lastName, this._phone, this._email, this._password).then(
          response => {
              // console.log(response);
              if (response.statusCode == 200) {
                  this.appState.jwt = response.data!.token;
                  this.router!.navigateToRoute('home');
              } else {
                  this._errorMessage = response.statusCode.toString()
                      + ' '
                      + response.errorMessage!
              }
          }
      );
  }

}
