import { IAlertData } from 'types/IAlertData';
import { AlertType } from '../types/AlertType';

export let SOURCE = {
    APP : {value: 1}, 
    ACCOUNT: {value: 2}, 
    CART : {value: 3},
    CARTMEAL : {value: 4},
    CATEGORY : {value: 5},
    COMPONENT : {value: 6},
    COMPONENTPIZZATPL : {value: 7},
    COMPONENTPIZZAUSER : {value: 8},
    HOME : {value: 9},
    ITEM : {value: 10},
    MEAL : {value: 11},
    PIZZA : {value: 12},
    PIZZATEMPLATE : {value: 13},
    PIZZAUSER : {value: 14},
    RESTAURANT : {value: 15},
    RESTAURANTFOOD : {value: 16},
    SHARING : {value: 17},
    SHARINGITEM : {value: 18},
    USERLOCATION : {value: 19}
  };

export function alertHandler(source: {value : number},
                            statusCode: number | null,
                            errorMessage: string | undefined): IAlertData | null {
    let alert: IAlertData | null;
    if (statusCode && statusCode >= 200 && statusCode < 300) {
        alert = null;
    }
    else if (!statusCode) {
        alert = {
            message: "Data loading failed! Connection could not be establised.",
            type: AlertType.Danger,
            dismissable: true,
        }
    } else {
        alert = {
            message: statusCode.toString() + ' - ' + errorMessage,
            type: AlertType.Danger,
            dismissable: true,
        }
    }
    return alert;
}