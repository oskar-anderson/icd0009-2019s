export interface IUserLocation {
    id: string;
    district: string;
    streetName: string;
    buildingNumber: string;
    apartmentNumber: string |  null;
}

export interface IUserLocationCreate {
    district: string;
    streetName: string;
    buildingNumber: string;
    apartmentNumber: string |  null;
}