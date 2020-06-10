export interface IRestaurant {
    id: string;
    name: string;
    location: string;
    telephone: string;
    openTime: string;
    openNotification: string;
}

export interface IRestaurantCreate {
    name: string;
    location: string;
    telephone: string;
    openTime: string;
    openNotification: string;
    
}