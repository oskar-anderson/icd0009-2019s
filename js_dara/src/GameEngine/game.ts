import { Player } from './player';
import { getClassListClass } from '../Utils/utils';

export class Game {
    BlackPlayer: Player;
    WhitePlayer: Player;
    ActivePlayer: Player;
    InActivePlayer: Player;
    Phase: number;
    PreviousEvent: null | MouseEvent;

    constructor(BlackPlayerIsAi: Boolean, WhitePlayerIsAi: Boolean, BlackStarts: Boolean){
        this.BlackPlayer = new Player("Black", BlackPlayerIsAi, 'X');
        this.WhitePlayer = new Player("White", WhitePlayerIsAi, 'O');
        this.ActivePlayer = BlackStarts ? this.BlackPlayer : this.WhitePlayer;
        this.InActivePlayer = BlackStarts ? this.WhitePlayer : this.BlackPlayer;
        this.Phase = 1;
        this.PreviousEvent = null;
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

    getActivePlayerMsg = () => this.ActivePlayer.Name + " player's turn";

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

    static grid = () => <HTMLElement[]> Array.from(document.getElementsByClassName('q'));    // get all quadrents
    static bloat = () => Game.grid().map(x => x.innerHTML)
    static emptyQs = () => Game.grid().filter(qEl => Game.tileIsEmpty(qEl));     // get empty quadrents
    static legalPlacementQs = (symbol: string) => Game.emptyQs().filter(qEl => ! Game.isWinningCombo(symbol, Game.bloat(), Game.gridIndex(qEl), true));
    static myQs = (game: Game) => Game.grid().filter(qEl => qEl.innerText === game.ActivePlayer.Symbol);
    static oppQs = (game: Game) => Game.grid().filter(qEl => qEl.innerText === game.InActivePlayer.Symbol);
    static tileIsEmpty = (qEl: HTMLElement) => qEl.innerText === '';
    static gridIndex = (qEl: HTMLElement ) => Number.parseInt(qEl.id.replace('q', ''));    // get quadrent index
    static RNG = (min: number, max: number) => Math.floor(Math.random() * (max - min + 1) ) + min;

    swapTurnAndAddPiece() {
        this.ActivePlayer.PlacedPieces++;
        this.swapTurn();
        if (this.BlackPlayer.PlacedPieces === 12 && this.WhitePlayer.PlacedPieces === 12) {
            this.Phase = 2;
        }
    }

    swapTurn() {
        [this.ActivePlayer, this.InActivePlayer] = [this.InActivePlayer, this.ActivePlayer];
    }
    
    private static shuffle(a: HTMLElement[]): void {
        for (let i = a.length - 1; i > 0; i--) {
            const j = Math.floor(Math.random() * (i + 1));
            [a[i], a[j]] = [a[j], a[i]];
        }
    }

    GetFromToSwap(myQsArr: HTMLElement[]): HTMLElement[] {
        Game.shuffle(myQsArr);
        console.log('shuffled array', myQsArr);
        const boardGrid: HTMLElement[] = Game.grid();
        const bloatGrid: string[] = Game.bloat();
        
        console.log('checking moves that cause combos');
        for(const oldTile of myQsArr) { 
            console.log('checking combo from', oldTile);
            const oldIndex = Game.gridIndex(oldTile);
            const row = Number.parseInt(getClassListClass(oldTile, 1).replace('r', ''));
            const column = Number.parseInt(getClassListClass(oldTile, 2).replace('c', ''));
           
            oldTile.innerHTML = '';
            let newTile;
    
            if(column < 5 && (oldIndex + 1 < 30)) {     // right
                newTile = boardGrid[oldIndex + 1];
                if (newTile.innerHTML === '') {
                    if (! Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 4)
                    && Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 3)) {
                        console.log('Found combo');
                        return [oldTile, newTile];
                    }  
                }
            } 
            if(column > 0 && (oldIndex - 1 > -1)) {     // left
                newTile = boardGrid[oldIndex - 1];
                if(newTile.innerHTML === '') {
                    if (! Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 4)
                    && Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 3)) {
                        console.log('Found combo');
                        return [oldTile, newTile];
                    }
                }
            } 
            if(row < 4 && (oldIndex + 6 < 30)) {        // down
                newTile = boardGrid[oldIndex + 6];
                if(newTile.innerHTML === '') {
                    if (! Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 4)
                    && Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 3)) {
                        console.log('Found combo');
                        return [oldTile, newTile];
                    }
                }
            } 
            if(row > 0 && (oldIndex - 6 > -1)) {        // up
                newTile = boardGrid[oldIndex - 6];
                if (newTile.innerHTML === '') {
                    if (! Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 4)
                    && Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 3)) {
                        console.log('Found combo');
                        return [oldTile, newTile];
                    }
                }
            }
            console.log('no combo');
            oldTile.innerHTML = this.ActivePlayer.Symbol;
        }
        console.log('checking random legal moves');
        for(let i = 0; i < myQsArr.length; ++i) {
            const oldTile: HTMLElement = myQsArr[i];
            console.log('checking random legal move from', oldTile);
            const oldIndex: number = Game.gridIndex(oldTile);
    
            const row: number = Number.parseInt(getClassListClass(oldTile, 1).replace('r', ''));
            const column: number = Number.parseInt(getClassListClass(oldTile, 2).replace('c', ''));
            
            oldTile.innerHTML = '';
    
            let newTile: HTMLElement = boardGrid[oldIndex + 1];
            if((column < 5 && (oldIndex + 1 < 30) && newTile.innerHTML === '')
            && ! Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 4)) {
                return [oldTile, newTile];
            } 
            newTile = boardGrid[oldIndex - 1];
            if((column > 0 && (oldIndex - 1 > -1) && newTile.innerHTML === '')
            && ! Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 4)) {
                return [oldTile, newTile];
            } 
            newTile = boardGrid[oldIndex + 6];
            if((row < 4 && (oldIndex + 6 < 30) && newTile.innerHTML === '')
            && ! Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 4)) {
                return [oldTile, newTile];
            } 
            newTile = boardGrid[oldIndex - 6];
            if((row > 0 && (oldIndex - 6 > -1) && newTile.innerHTML === '')
            && ! Game.causesCombo(newTile, this.ActivePlayer.Symbol, bloatGrid, 4)) {
                return [oldTile, newTile];
            }
            oldTile.innerHTML = this.ActivePlayer.Symbol;
        }
        alert('No choice');
        throw new Error('No choice');
    }

    phaseIPlayer(event: MouseEvent, indexPrimary: number) {
        if (Game.tileIsEmpty(<HTMLElement> event.target)) {
            if(! Game.isWinningCombo(this.ActivePlayer.Symbol, Game.bloat(), indexPrimary, false)) {
                this.placePiece(Game.grid(), indexPrimary, this.ActivePlayer.Symbol);
                //viewUpdate();
            }
            console.log('Cannot place piece there, 3-in-a-row is not allowed in phase one.');
        }
        return null;
    }
    
    phaseIIPlayer(event: MouseEvent, indexPrimary: number) {
        console.log('Player move');
        if(this.PreviousEvent === null) { 
            this.PreviousEvent = event;
            console.log('Swap this', event.target, ' for...');
            //viewUpdate();
            return null;
        }
        console.log('Attempting to swap ', this.PreviousEvent.target, event.target);
        let indexSecondary = Game.gridIndex(<HTMLElement> this.PreviousEvent.target);
        
        let swapLegalBool = this.swapLegalCheck(<HTMLElement> event.target, <HTMLElement> this.PreviousEvent.target);
        if (! swapLegalBool) {
            console.log('Swap is illegal.');
            this.PreviousEvent = null;
            //viewUpdate();
            return null;
        }
        let IllegalComboBool = Game.isIllegalCombo(this.ActivePlayer.Symbol, Game.bloat(), indexPrimary, indexSecondary, false);
        if (IllegalComboBool) {
            console.log('Swap creates illegal combo of atleast 4.');
            this.PreviousEvent = null;
            //viewUpdate();
            return null;
        }
    
        console.log('Swapping');
        let swapTemp = Game.grid()[indexPrimary].innerText;
        Game.grid()[indexPrimary].innerText = (<HTMLElement> this.PreviousEvent.target).innerHTML;
        Game.grid()[indexSecondary].innerText = swapTemp;
        this.PreviousEvent = null;
        
        if(Game.causesCombo(<HTMLElement> event.target, this.ActivePlayer.Symbol, Game.bloat(), 3)){
            this.Phase = 3;
        }
        else{
            this.swapTurn();
        }
        //viewUpdate();
        return null;
    }
    
    phaseIIIPlayer(event: MouseEvent, indexPrimary: number): string {
        let targetIsOppPiece = (<HTMLElement> event.target).innerHTML === this.InActivePlayer.Symbol;
                
        if (! targetIsOppPiece) {
            console.log('Piece must belong to opponent.');
            return 'Remove failed';
        }
        let targetIsPartOfCombo = Game.causesCombo(<HTMLElement> event.target, this.InActivePlayer.Symbol, Game.bloat(), 3);
        if (targetIsPartOfCombo) {
            console.log('Cannot remove combod pieces', event.target);
            return 'Remove failed';
        }
        else {
            Game.grid()[indexPrimary].innerText = '';
            this.InActivePlayer.PlacedPieces -= 1;
            this.swapTurn();
            console.log('removed piece', event.target)
        }
        return '';
    }
    
    phaseIBot() {
        let legalPlacementQsVar = Game.legalPlacementQs(this.ActivePlayer.Symbol);
        console.log('All legal placemnents:', legalPlacementQsVar);
        let oppChoice = Game.gridIndex(legalPlacementQsVar[Math.floor(Math.random() * legalPlacementQsVar.length)]);
        console.log('AI Opponent choice', Game.bloat()[oppChoice]);
        
        this.placePiece(Game.grid(), oppChoice, this.ActivePlayer.Symbol);
        return '';
    }

    phaseIIBot() {
        console.log('Bot move');
        const [oldTile, newTile] = this.GetFromToSwap(Game.myQs(this));
    
        console.log('Moving form', oldTile, 'to', newTile);
        [newTile.innerHTML, oldTile.innerHTML] = [this.ActivePlayer.Symbol, newTile.innerHTML];
        if(Game.causesCombo(newTile, this.ActivePlayer.Symbol, Game.bloat(), 3)) {
            this.Phase = 3;
        }
        if(this.Phase !== 3) {
            console.log('swapped turn');
            this.swapTurn();
        }
        return '';
    }

    phaseIIIBot (): string {
        let oppQsArr = Game.oppQs(this);
        console.log('oppQsArr', oppQsArr);
        let oppNoComboQsArr = oppQsArr.filter(x => ! Game.causesCombo(x, this.InActivePlayer.Symbol, Game.bloat(), 3));
        console.log('oppNoComboArr', oppNoComboQsArr);
        let tileToDestroy = oppNoComboQsArr[Game.RNG(0, oppNoComboQsArr.length - 1)];
        console.log('removed piece', tileToDestroy);
        if (tileToDestroy === undefined) { 
            alert('Game Over! Cannot make any legal moves. Tie!');
        }
        tileToDestroy.innerHTML = '';
        this.InActivePlayer.PlacedPieces -= 1;
        this.swapTurn();
        return '';
    }


    placePiece = (grid: HTMLElement[], index: number, letter: string) => { 
        grid[index].innerText = letter;
        this.swapTurnAndAddPiece();
    }
    

    swapLegalCheck(currentTarget: HTMLElement, previousTarget: HTMLElement) {
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
        
        console.log('Swap is allowed!');
        return true;
        
    }

    static adjacencyCheck(currentTarget: HTMLElement, previousTarget: HTMLElement) {
        const el1Row = Number.parseInt(getClassListClass(currentTarget, 1).replace('r', ''));
        const el1Col = Number.parseInt(getClassListClass(currentTarget, 2).replace('c', ''));
        const el2Row = Number.parseInt(getClassListClass(previousTarget, 1).replace('r', ''));
        const el2Col = Number.parseInt(getClassListClass(previousTarget, 2).replace('c', ''));

        if((el1Row === el2Row && (el1Col === el2Col - 1 || el1Col === el2Col + 1)) || 
        (el1Col === el2Col && (el1Row === el2Row - 1 || el1Row === el2Row + 1))) {
            return true;
        }
        return false;
    }

    static causesCombo(piece: HTMLElement, pieceSymbol: string, grid: string[], maxComboLenght: number) {
        const row = Number.parseInt(piece.classList.item(1)!.replace('r', ''));
        const column = Number.parseInt(piece.classList.item(2)!.replace('c', ''));
        const index = row * 6 + column;
    
        let comboLenght = 1;
        
        for(let i = 0; i < 2; i++) {    // horizontal __X__
            for(let x = 1; x < maxComboLenght; x++) {
                const moveBy = (i === 0) ? x : -x;  // positive then negative
                if(column + moveBy < 6 && column + moveBy >= 0 && grid[index + moveBy] === pieceSymbol) 
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
                if(row + moveBy < 5 && row + moveBy >= 0 && grid[index + moveBy * 6] === pieceSymbol) 
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

    static isIllegalCombo(piece: string, grid: string[], currentIndex: number, previousIndex: number, supMsg: Boolean) { 
        [grid[previousIndex], grid[currentIndex]] = ['', piece];
        let causesCombo = Game.comboExist(4, piece, grid, supMsg);
        [grid[previousIndex], grid[currentIndex]] = [piece, ''];
        return causesCombo;
    }

    static isWinningCombo(piece: string, grid: string[], index: number, supMsg: Boolean) {
        grid[index] = piece;
        let causesCombo = Game.comboExist(3, piece, grid, supMsg);
        grid[index] = '';
        return causesCombo;
    }

    static comboExist(lenght: number, piece: string, grid: string[], supMsg: Boolean) {
        // check horizontal
        // console.log('check horizontal');
        for (let y = 0; y < 5; y++) {
            for (let x = 0; x < 6 - (lenght - 1); x++) {
                let line = 0;
                for (let i = 0; i < lenght; i++) { 
                    if (grid[(y * 6) + (x + i)] === piece) { 
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
                    if (grid[((y + i) * 6) + x] === piece) { 
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
