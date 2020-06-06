import { IAlertData } from 'types/IAlertData';
import { AlertType } from '../types/AlertType';

export let SOURCE = {
    APP : {value: "App"}, 
    ACCOUNT: {value: "Account"}, 
    CART : {value: "Cart"},
    CARTMEAL : {value: "Cart Meal"},
    CATEGORY : {value: "Category"},
    COMPONENT : {value: "Component"},
    COMPONENTPIZZATPL : {value: "Component Pizza Template"},
    COMPONENTPIZZAUSER : {value: "Component Pizza User"},
    HOME : {value: "Home"},
    ITEM : {value: "Item"},
    MEAL : {value: "Meaal"},
    PIZZA : {value: "Pizza"},
    PIZZATEMPLATE : {value: "Pizza Template"},
    PIZZAUSER : {value: "Pizza User"},
    RESTAURANT : {value: "Restaurant"},
    RESTAURANTFOOD : {value: "Restaurant Food"},
    SHARING : {value: "Sharing"},
    SHARINGITEM : {value: "Sharing Item"},
    USERLOCATION : {value: "User Location"}
  };

export function alertHandler(
        source: {value : string},
        statusCode: number | null,
        errorMessage: string | undefined): IAlertData | null {

    let alert: IAlertData | null;
    if (statusCode && statusCode >= 200 && statusCode < 300) {
        alert = null;
    }
    else if (!statusCode) {
        alert = {
            message: "We have a API down! Connection with entity " + source.value + " could not be establised.",
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