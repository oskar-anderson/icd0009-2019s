export interface ISharingItem {
    id: string;
    sharingId: string;
    itemId: string,
    friendName: string;
    percent: number;
    friendOwns: number;

    itemName: string;
}

export interface ISharingItemCreate {
    sharingId: string;
    itemId: string,
    friendName: string;
    percent: number;
    friendOwns: number;
}