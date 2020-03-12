export class Tile{
    constructor(y, x, piece){
        this.y = y;
        this.x = x;
        this.piece = piece;
        this.swappable = false;
    }

    toString() {
        return this.piece;
    }
}