import { Player } from './player.js';

export class Game {
    constructor(BlackPlayerIsAi, WhitePlayerIsAi, BlackStarts){
        this.BlackPlayer = new Player("Black", BlackPlayerIsAi, 'X');
        this.WhitePlayer = new Player("White", WhitePlayerIsAi, 'O');
        this.ActivePlayer = BlackStarts ? this.BlackPlayer : this.WhitePlayer;
        this.InActivePlayer = BlackStarts ? this.WhitePlayer : this.BlackPlayer;
        this.Phase = 1;
        this.PreviousEvent = null;
    }

    swapTurnAndAddPiece() {
        this.ActivePlayer.PlacedPieces++;
        [this.ActivePlayer, this.InActivePlayer] = [this.InActivePlayer, this.ActivePlayer];
        if (this.BlackPlayer.PlacedPieces === 12 && this.WhitePlayer.PlacedPieces === 12) {
            this.Phase = 2;
        }
    }

    swapTurn() {
        [this.ActivePlayer, this.InActivePlayer] = [this.InActivePlayer, this.ActivePlayer];
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
        return this.ActivePlayer.Name + " player's turn";
    }

    getGameModeMsg() {
        if ((this.BlackPlayer.IsAiPlayer && ! this.WhitePlayer.IsAiPlayer) ||
            ! this.BlackPlayer.IsAiPlayer && this.WhitePlayer.IsAiPlayer) {
            return 'Singleplayer';
        }
        if (! this.BlackPlayer.IsAiPlayer && ! this.WhitePlayer.IsAiPlayer) {
            return 'Multiplayer';
        }
        if (this.BlackPlayer.IsAiPlayer && this.WhitePlayer.IsAiPlayer) {
            return 'AI';
        }
        return 'error';
    }

    swapLegalCheck(currentTarget, previousTarget) {
        if (currentTarget.innerHTML === previousTarget.innerHTML) {
            console.log('Swap cancelled, pieces are the same.');
            return false;
        }
        if(currentTarget.innerHTML !== '') { 
            console.log('Swap cancelled, target tile occupied.');
            return false;
        }
        if(this.ActivePlayer.Symbol !== previousTarget.innerHTML) { 
            console.log('Swap cancelled, pick up your game piece.');
            return false;
        }
        if (! Game.adjacencyCheck(currentTarget, previousTarget)) {
            console.log('Swap cancelled, Tiles are not adjacent.');
            return false;
        }
        
        console.log('Swap is allowed!')
        return true;
        
    }

    static adjacencyCheck(currentTarget, previousTarget) {
        let el1Row = Number.parseInt(currentTarget.classList.item(1).replace('r', ''));
        let el1Col = Number.parseInt(currentTarget.classList.item(2).replace('c', ''));
        let el2Row = Number.parseInt(previousTarget.classList.item(1).replace('r', ''));
        let el2Col = Number.parseInt(previousTarget.classList.item(2).replace('c', ''));

        if(
        (el1Row === el2Row && (el1Col === el2Col - 1 || el1Col === el2Col + 1)) || 
        (el1Col === el2Col && (el1Row === el2Row - 1 || el1Row === el2Row + 1))) {
            return true;
        }
        return false;
    }

    static causesCombo(piece, pieceSymbol, grid, maxComboLenght) {
        const row = Number.parseInt(piece.classList.item(1).replace('r', ''));
        const column = Number.parseInt(piece.classList.item(2).replace('c', ''));
        const index = row * 6 + column;
    
        let comboLenght = 1;
        
        for(let i = 0; i < 2; i++) {    // horizontal __X__
            for(let x = 1; x < maxComboLenght; x++) {
                const moveBy = (i === 0) ? x : -x;  // positive then negative
                if(column + moveBy < 6 && column + moveBy >= 0 && grid[index + moveBy].innerHTML === pieceSymbol) 
                    comboLenght++;
                else break;
            }
        } if(comboLenght === maxComboLenght) {
            // console.log("Placement causes horizontal combo of", comboLenght);
            return true;
        }
        
        comboLenght = 1;
        
        for(let i = 0; i < 2; i++) {    // vertical __X__
            for(let y = 1; y < maxComboLenght; y++) {
                const moveBy = (i === 0) ? y : -y;
                if(row + moveBy < 5 && row + moveBy >= 0 && grid[index + moveBy * 6].innerHTML === pieceSymbol) 
                    comboLenght++;
                else break;
            }
        } if(comboLenght === maxComboLenght) {
            // console.log("Placement causes vertical combo of", comboLenght);
            return true;
        } 
        
        // console.log("Placement didn't cause a combo.");
        return false;
    }

    static isIllegalCombo(piece, grid, currentIndex, previousIndex, supMsg) { 
        grid[previousIndex].innerText = '';
        grid[currentIndex].innerText = piece;
        let causesCombo = Game.comboExist(4, piece, grid, supMsg);
        grid[previousIndex].innerText = piece;
        grid[currentIndex].innerText = '';
        return causesCombo;
    }

    static isWinningCombo(piece, grid, index, supMsg) {
        grid[index].innerText = piece;
        let causesCombo = Game.comboExist(3, piece, grid, supMsg);
        grid[index].innerText = '';
        return causesCombo;
    }

    static comboExist(lenght, piece, grid, supMsg) {
        // check horizontal
        // console.log('check horizontal');
        // console.dir((grid[0].innerHTML === 'X'));
        for (let y = 0; y < 5; y++) {
            for (let x = 0; x < 6 - (lenght - 1); x++) {
                let line = 0;
                for (let i = 0; i < lenght; i++) { 
                    if (grid[(y * 6) + (x + i)].innerHTML === piece) { 
                        line++;
                    } 
                }
                if (line === lenght) { 
                    if(! supMsg) { console.log('Horizontal ' + lenght + ' in-a-row'); }
                    return true; 
                }
            }
        }
        
        // check vertical
        // console.log('check vertical');
        for (let y = 0; y < 5 - (lenght - 1); y++){
            for (let x = 0; x < 6; x++){
                let line = 0;
                for (let i = 0; i < lenght; i++) { 
                    if (grid[((y + i) * 6) + x].innerHTML === piece) { 
                        line++;
                    } 
                }
                if (line === lenght) { 
                    if(! supMsg) { console.log('Vertical ' + lenght + ' in-a-row'); }
                    return true; 
                }
            }
        }
        if(! supMsg) { console.log('No ' + lenght + ' in-a-row'); }
        return false;
    }
}
