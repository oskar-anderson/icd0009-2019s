import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

import { SharingService} from 'service/sharing-service';
import { ISharing } from 'domain/Isharing';

import { ItemService} from 'service/item-service';
import { IItem } from 'domain/Iitem';

import { SharingItemService} from 'service/sharingItem-service';
import { ISharingItem } from 'domain/ISharingItem';

import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { alertHandler, SOURCE } from 'service/alert-service';

@autoinject
export class SharingEdit {
    private _alert: IAlertData | null = null;

    private _sharing!: ISharing;
    private _itemsOfShare: IItem[] = [];
    private _activeItem: IItem | undefined = undefined;
    private _sharingItems: ISharingItem[] = []
    private _friendName: string = "";

    private _maxOutput: number = 100;
    private _maxShare: number = 0;
    private _output: number = this._maxOutput / 2;
    private _share: number = 0;

    constructor(
        private sharingService: SharingService, 
        private itemService: ItemService,
        private sharingItemService: SharingItemService,
        private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.sharingService.getSharing(params.id).then(
                response => {
                    this._alert = alertHandler(SOURCE.SHARING, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._sharing = response.data!;
                        this.sharedLoad()
                    };
                }
            );
        }
    }

    sharedLoad() {
        this.sharingItemService.getSharingItems().then(
            response => {
                this._alert = alertHandler(SOURCE.ITEM, response.statusCode, response.errorMessage);
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._sharingItems = response.data!;

                    this.itemService.getItems().then(
                        response => {
                            this._alert = alertHandler(SOURCE.ITEM, response.statusCode, response.errorMessage);
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                let allItems = response.data!;
                                this._itemsOfShare = allItems.filter(x => x.sharingId === this._sharing.id);
                                this.setItems();
                                if (this._itemsOfShare.length !== 0) {
                                    this._activeItem = this._sharing.items[0]!;
                                    this.ItemChange()
                                    this._share = this._activeItem.gross * this._output / 100
                                    this._share = Math.round((this._share + Number.EPSILON) * 100) / 100;
                                }
                            }
                        }
                    );
                }
            }
        );
    }

    setItems() {
        this._sharing.sharingItems = [];
        this._sharing.items = []
        for (const item of this._itemsOfShare) {
            let sharingItems = this._sharingItems.filter(x => x.itemId === item.id)
            for (const sharingItem of sharingItems) {
                sharingItem.itemName = item.name;
            }
            this._sharing.sharingItems.push(...sharingItems)
            let friendOwnsTotal = this.getFriendOwns(sharingItems);
            if (friendOwnsTotal < 100) {
                this._sharing.items.push(item);
            }
        }
    }

    getFriendOwns(sharingItems: ISharingItem[]){
        let friendOwnsTotal = 0;
        for (const sharingItem of sharingItems) {
            friendOwnsTotal += sharingItem.percent;
        }
        friendOwnsTotal = Math.round((friendOwnsTotal + Number.EPSILON) * 100) / 100;
        return friendOwnsTotal;
    }

    updateValue() {
        let slider = document.getElementById("myRange")! as HTMLFormElement;
        this._output = slider.value;
        console.log(this._output);
        this._share = this._activeItem!.gross * this._output / 100
        this._share = Math.round((this._share + Number.EPSILON) * 100) / 100;

        let offSetRatio = 100 / this._maxOutput
        console.log(slider.value *  100 / this._maxOutput === slider.value * offSetRatio)   // true
        slider.addEventListener("mousemove", function() {
            let cutoff = slider.value * offSetRatio;            // WTF
            console.log(slider.value * 100 / this._maxOutput)   // Nan
            console.log(slider.value * offSetRatio)             // number
            let color = 'linear-gradient(90deg, rgb(117, 252, 117)' + cutoff + '% , rgb(214, 214, 214)' + cutoff + '%)';
            slider.style.background = color;
        });
    }

    ItemChange() {
        let activeSharingItems = this._sharingItems.filter(x => x.itemId === this._activeItem!.id)
        let friendOwnsTotal = this.getFriendOwns(activeSharingItems)
        this._maxOutput = 100 - friendOwnsTotal;
        this._maxShare = this._maxOutput / 100 * this._activeItem!.gross;
        this._maxShare = Math.round((this._maxShare + Number.EPSILON) * 100) / 100;
        this._output = this._maxOutput / 2;
    }

    onSubmit(event: Event) {
        if (this._friendName !== "" && this._output !== 0) {

            this.sharingItemService
                .createSharingItem({
                    sharingId: this._sharing.id,
                    itemId: this._activeItem!.id,
                    friendName: this._friendName,
                    percent: parseFloat(this._output + ""),
                    friendOwns: parseFloat(this._share + ""),
                })
                .then(
                    response => {
                        this._alert = alertHandler(SOURCE.SHARINGITEM, response.statusCode, response.errorMessage);
                        if (response.statusCode >= 200 && response.statusCode < 300) {
                            this.sharedLoad()
                        }
                    }
                );
    
            event.preventDefault();
        }
    }

    deleteSharingItem(sharingItem: ISharingItem) {
        this.sharingItemService
            .deleteSharingItem(sharingItem.id)
            .then(
                response => {
                    this._alert = alertHandler(SOURCE.SHARINGITEM, response.statusCode, response.errorMessage);
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.sharedLoad()
                    }
                }
            );
    }
}
