import { Tile } from './tile.js';
import { Player } from './player.js';

export class Game {
    constructor(RedPlayerIsAi, YelPlayerIsAi, RedStarts){
        this.RedPlayer = new Player("red", RedPlayerIsAi);
        this.YelPlayer = new Player("yellow", YelPlayerIsAi);
        this.ActivePlayer = RedStarts ? this.RedPlayer : this.YelPlayer;
        this.InActivePlayer = () => this.RedPlayer === this.ActivePlayer ? this.YelPlayer : this.RedPlayer;
        this.Board = this.generateBoard();
        this.Phase = 1;
    }

    swapTurn() {
        this.ActivePlayer.PiecesToPlace--;
        this.ActivePlayer = this.ActivePlayer === this.RedPlayer ? this.YelPlayer : this.RedPlayer;
        if (this.RedPlayer.PiecesToPlace === 0 || this.YelPlayer.PiecesToPlace === 0) {
            this.Phase = 2;
        }
    }

    generateBoard(){
        let board = [];
        for (let rowCount = 0; rowCount < 5; rowCount++) {
            let boardRow = [];
            for (let columnCount = 0; columnCount < 6; columnCount++) {
                let tile = new Tile(rowCount, columnCount, 'E');
                boardRow.push(tile);
            }
            board.push(boardRow)
        }
        return board;
    }

    getPhaseMsg(){
        if (this.Phase === 1){
            return 'Phase - placement';
        }
        if (this.Phase === 2){
            return 'Phase - swapping';
        }
        if (this.Phase === 3){
            return 'Phase - removal';
        }
        return 'error';
    }

    getActivePlayerMsg() {
        if (this.ActivePlayer === this.RedPlayer) {
            return "Red player's turn";
        }
        if (this.ActivePlayer === this.YelPlayer) {
            return "Yellow player's turn";
        }
        return 'error';
    }

    getVictoryMsg() {
        // todo
        return '';
    }

}

function getIsIllegalCombo(piece, grid) {
    return comboExist(4, piece, grid);
}

function getIsWinningCombo(piece, grid) {
    return comboExist(3, piece, grid);
}

function comboExist(lenght, piece, grid) {
    // check horizontal
    for (let y = 0; y < 5; y++) {
        for (let x = 0; x < 6; x++) {
            let line = 0;
            for (let i = 0; i < lenght; i++) { 
                if (grid[y * 6, x + i] === piece) { 
                    line++; 
                } 
            }
            if (line == lenght) { 
                return true; 
            }
        }
    }

    // check vertical
    for (let y = 0; y < 5 - (lenght - 1); y++){
        for (let x = 0; x < 6; x++){
            let line = 0;
            for (let i = 0; i < lenght; i++) { 
                if (grid[(y + i) * 6, x] === piece) { 
                    line++; 
                } 
            }
            if (line == lenght) { 
                return true; 
            }
        }
    }
    return false;
}
