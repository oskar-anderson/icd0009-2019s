export class Player {
    Name: string;
    Symbol: string;
    IsAiPlayer: Boolean;
    PlacedPieces: number;
    constructor(Name: string, IsAiPlayer: Boolean, Symbol: string){
        this.Name = Name;
        this.Symbol = Symbol;
        this.IsAiPlayer = IsAiPlayer;
        this.PlacedPieces = 0;
    }

    getPiecesToPlaceMsg() {
        return this.Name + " " + this.Symbol + " " + "(" + this.PlacedPieces + " / 12)";
    }

}