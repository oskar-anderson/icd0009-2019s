export class Player {
    constructor(Name, IsAiPlayer, Symbol){
        this.Name = Name;
        this.Symbol = Symbol;
        this.IsAiPlayer = IsAiPlayer;
        this.PlacedPieces = 0;
    }

    getPiecesToPlaceMsg() {
        return this.Name + " " + this.Symbol + " " + "(" + this.PlacedPieces + " / 12)";
    }

}