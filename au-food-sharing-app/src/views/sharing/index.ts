import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';

import { ISharing } from './../../domain/ISharing';
import { SharingService } from 'service/sharing-service';

import { IItem } from './../../domain/IItem';
import { ItemService } from 'service/item-service';

import { SharingItemService} from 'service/sharingItem-service';
import { ISharingItem } from 'domain/ISharingItem';

import { IAlertData } from 'types/IAlertData';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class SharingIndex{
    private _sharings: ISharing[] = [];
    private _items: IItem[] = []
    private _sharingItems: ISharingItem[] = []

    private _alert: IAlertData | null = null;

    constructor(
        private sharingService: SharingService,
        private itemService: ItemService,
        private sharingItemService: SharingItemService){

    }

    attached() {
        this.sharingService.getSharings().then(
            response => {
                this._alert = alertHandler(SOURCE.SHARING, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._sharings = response.data!;
                    
                    

                }
            }
        );
    }

    deleteSharing(sharing: ISharing) {
        this.sharingItemService.getSharingItems().then(
            response => {
                this._alert = alertHandler(SOURCE.ITEM, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._sharingItems = response.data!;

                    this.itemService.getItems().then(
                        async response => {
                            this._alert = alertHandler(SOURCE.ITEM, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                this._items = response.data!;
                                console.log(1);
                                let sharingItemsToDelete = this._sharingItems.filter(x => x.sharingId === sharing.id)
                                let sharingItemDeleteResults: boolean[] = [];
                                for (const sharingItem of sharingItemsToDelete) {
                                    await this.sharingItemService
                                        .deleteSharingItem(sharingItem.id)
                                        .then(
                                            response => {
                                                this._alert = alertHandler(SOURCE.SHARINGITEM, response.statusCode, response.errorMessage);
                                                console.log("sharingItem" + response.statusCode);
                                                if (response.statusCode >= 200 && response.statusCode < 300) {
                                                    sharingItemDeleteResults.push(true);
                                                }
                                                else {
                                                    sharingItemDeleteResults.push(false);
                                                }
                                            }
                                        );
                                }
                                let failed = false;
                                for (const deleteResult of sharingItemDeleteResults) {
                                    if (deleteResult === false) {
                                        failed = true;
                                        break
                                    }
                                }
                                if (!failed || sharingItemDeleteResults.length === 0) {
                                    console.log(2);
                                    let itemDeleteResults: boolean[] = [];
                                    let itemsToDelete = this._items.filter(x => x.sharingId === sharing.id)
                                    for (const item of itemsToDelete) {
                                        await this.itemService
                                            .deleteItem(item.id)
                                            .then(
                                                response => {
                                                    this._alert = alertHandler(SOURCE.ITEM, response.statusCode, response.errorMessage);
                                                    console.log("item" + response.statusCode);
                                                    if (response.statusCode >= 200 && response.statusCode < 300) {
                                                        itemDeleteResults.push(true);
                                                    }
                                                    else {
                                                        itemDeleteResults.push(false);
                                                    }
                                                }
                                            );
                                            
                                    }
                                    failed = false;
                                    for (const deleteResult of itemDeleteResults) {
                                        if (deleteResult === false) {
                                            failed = true;
                                            break
                                        }
                                    }
                                    if (!failed || sharingItemDeleteResults.length === 0) {
                                        console.log(3);
                                        await this.sharingService
                                            .deleteSharing(sharing.id)
                                            .then(
                                                response => {
                                                    this._alert = alertHandler(SOURCE.SHARING, response.statusCode, response.errorMessage);
                                                    console.log("share" + response.statusCode);
                                                    if (response.statusCode >= 200 && response.statusCode < 300) {
                                                        this.sharingService.getSharings().then(
                                                            response => {
                                                                this._alert = alertHandler(SOURCE.SHARING, response.statusCode, response.errorMessage);
                                                                if (response.statusCode >= 200 && response.statusCode < 300) {
                                                                    this._sharings = response.data!;
                                                                }
                                                            }
                                                        );
                                                    }
                                                }
                                            );
                                    }
                                }
                            }
                        }
                    );
                }
            }
        );
    }

}