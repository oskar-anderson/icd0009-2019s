export class Player {
    constructor(Name, IsAiPlayer){
        this.Name = Name;
        this.Symbol = Name === 'red' ? 'X' : 'O';
        this.IsAIPlayer = IsAiPlayer;
        this.PiecesToPlace = 12;
    }

    getPiecesToPlaceMsg() {
        return this.Name.charAt(0).toUpperCase() + this.Name.substring(1) + "(" + this.PiecesToPlace + " / 12)";
    }
}