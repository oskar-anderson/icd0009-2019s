import { IItem } from '../domain/IItem'
import { ISharingItem } from './ISharingItem';

export interface ISharing {
    id: string;
    name: string;

    items: IItem[];
    sharingItems: ISharingItem[];
}

export interface ISharingCreate {
    name: string;
}