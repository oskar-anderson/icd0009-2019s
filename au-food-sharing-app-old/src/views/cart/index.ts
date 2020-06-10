import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

import { ICartIndex } from './../../domain/ICart';
import { CartService } from 'service/cart-service';

import { ICartMeal } from './../../domain/ICartMeal';
import { CartMealService } from 'service/cartMeal-service';

import { IRestaurant } from './../../domain/IRestaurant';
import { RestaurantService } from 'service/restaurant-service';

import { IUserLocation } from './../../domain/IUserLocation';
import { UserLocationService } from 'service/userLocation-service';

import { ISharing } from './../../domain/ISharing';
import { SharingService } from 'service/sharing-service';

import { IItem } from './../../domain/IItem';
import { ItemService } from 'service/item-service';

import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';



@autoinject
export class CartIndex{
    private _carts: ICartIndex[] = [];
    private _cartMeals: ICartMeal[] = [];
    private _restaurants: IRestaurant[] = [];
    private _userLocations: IUserLocation[] = [];
    private _sharings: ISharing[] = [];


    private _alert: IAlertData | null = null;

    constructor(
        private cartService: CartService,
        private cartMealService: CartMealService,
        private restaurantService: RestaurantService,
        private userLocationService: UserLocationService,
        private sharingService: SharingService,
        private itemService: ItemService,
        private router: Router){

    }
    //restaurantName: string,
    //userLocationName: string,
    //invoice: number[];
    //total: number;

    attached() {
        this.cartService.getCartsIndex().then(
            response => {
                this._alert = alertHandler(SOURCE.CART, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._carts = response.data!;
                    this.cartMealService.getCartMeals().then(
                        response => {
                            this._alert = alertHandler(SOURCE.CART, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                this._cartMeals = response.data!;
                                
                                for (const cart of this._carts) {
                                    cart.sharingName = "My Sharing";
                                    let cartMeals = this._cartMeals.filter(x => x.cartId === cart.id)
                                    cart.cartMeals = [...cartMeals]
                                    let total = cart.cartMeals.map(x => x.totalGross).reduce((a, b) => a + b);
                                    cart.total = Math.round((total + Number.EPSILON) * 100) / 100;
                                };
                            }
                        }
                    );
                    this.restaurantService.getRestaurants().then(
                        response => {
                            this._alert = alertHandler(SOURCE.CART, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                this._restaurants = response.data!;
                                
                                for (const cart of this._carts) {
                                    let restaurant = this._restaurants.filter(x => x.id === cart.restaurantId)[0];
                                    cart.restaurantName = restaurant.name;
                                }
                            }
                        }
                    );
                    this.userLocationService.getUserLocations().then(
                        response => {
                            this._alert = alertHandler(SOURCE.CART, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                this._userLocations = response.data!;

                                for (const cart of this._carts) {
                                    let userLocation = this._userLocations.filter(x => x.id === cart.userLocationId)[0];
                                    cart.userLocation = userLocation;
                                }
                            }
                        }
                    );
                }
            }
        );
    }


    shareInvoice(cart: ICartIndex) {
        this.sharingService
            .createSharing({
                name: cart.sharingName,
                })
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.SHARING, response.statusCode, response.errorMessage);
                    console.log(response.statusCode, response.data, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.sharingService.getSharings().then(
                            response => {
                                this._alert = alertHandler(SOURCE.SHARING, response.statusCode, response.errorMessage);
                                if (response.statusCode >= 200 && response.statusCode < 300) {
                                    this._sharings = response.data!

                                    let responses: number[] = [];
                                    let sharing = this._sharings.slice(-1).pop()!;
                                    for (const cartMeal of cart.cartMeals) {
                                        this.itemService
                                            .createItem({
                                                sharingId: sharing.id,
                                                name: cartMeal.name,
                                                gross: cartMeal.totalGross,
                                                })
                                            .then(
                                                response => {
                                                    this._alert = alertHandler(SOURCE.ITEM, response.statusCode, response.errorMessage);
                                                    console.log(response.statusCode, response.data, response.errorMessage);
                                                    responses.push(response.statusCode);
                                                }   
                                            );
                                        
                                    }
                                    if (responses.every(x => x === responses[0])) {
                                        this.router.navigateToRoute('sharing-index', {});
                                    }
                                }
                            }
                        );
                    }
                }
            );
    }
}