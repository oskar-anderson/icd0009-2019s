export interface IItem {
    id: string;
    sharingId: string;
    name: string;
    gross: number;
}

export interface IItemCreate {
    sharingId: string;
    name: string;
    gross: number;
}