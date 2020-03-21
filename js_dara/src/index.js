import { Game } from './GameEngine/game.js';

let game = null;
const grid = () => Array.from(document.getElementsByClassName('q'));    // get all quadrents

const clickFn = (event) => {
    const indexPrimary = gridIndex(event.target);
    console.clear();
    console.log('grid', grid());
    // console.dir(grid()[0]);
    console.log('Game', game);
    console.log('Active player', game.ActivePlayer, 'Inactive player' ,game.InActivePlayer);
    console.log();
    console.log();
    console.log('User clicked on', event.target);

    if (game.Phase === 1) {
        console.log('Phase 1');
        // player vs AI / AI vs player 
        if((! game.ActivePlayer.IsAiPlayer && game.InActivePlayer.IsAiPlayer) || 
        (game.ActivePlayer.IsAiPlayer && ! game.InActivePlayer.IsAiPlayer)) {
            console.log('Player vs AI');
            if(game.ActivePlayer.IsAiPlayer){
                phaseIBot();
                return '';
            }
            else {
                phaseIPlayer(event, indexPrimary);
                return '';
            }
        }
        // player vs player
        else if(! game.ActivePlayer.IsAiPlayer && ! game.InActivePlayer.IsAiPlayer) {
            console.log('Player vs Player');
            phaseIPlayer(event, indexPrimary);
            return '';
        }
        // Ai vs Ai
        else if(game.ActivePlayer.IsAiPlayer && game.InActivePlayer.IsAiPlayer) {
            console.log('Ai vs Ai');
            phaseIBot();
            return '';
        }
    } if (game.Phase === 2) {
        console.log('Phase 2');

        // player vs AI / AI vs player 
        if((! game.ActivePlayer.IsAiPlayer && game.InActivePlayer.IsAiPlayer) || 
        (game.ActivePlayer.IsAiPlayer && ! game.InActivePlayer.IsAiPlayer)) {
            console.log('Player vs AI');
            if(game.ActivePlayer.IsAiPlayer){
                phaseIIBot();
                return '';
            } else {
                phaseIIPlayer(event, indexPrimary);
                return '';
            }
        }
        else if(game.ActivePlayer.IsAiPlayer && game.InActivePlayer.IsAiPlayer){
            console.log('AI VS AI');
            phaseIIBot();
            return '';
        }
        else {
            console.log('Player VS Player');
            phaseIIPlayer(event, indexPrimary);
            return '';
        }
    } if (game.Phase === 3) {
        console.log('Phase 3');
        console.log('Active player ' + game.ActivePlayer.Symbol);
        if(game.ActivePlayer.IsAiPlayer){
            phaseIIIBot();
        }
        else {
            if (phaseIIIPlayer(event,indexPrimary) === 'Remove failed') { return; }
        }
        game.Phase = 2;
        viewUpdate();

        // turn is already swapped
        if(game.ActivePlayer.PlacedPieces < 3 ) {
            disableListeners();
            alert('Game Over! ' + game.InActivePlayer.Name + ' won!');
        }
    }
}

function phaseIBot() {
    let legalPlacementQsVar = legalPlacementQs();
    console.log('All legal placemnents:', legalPlacementQsVar);
    let oppChoice = gridIndex(legalPlacementQsVar[Math.floor(Math.random() * legalPlacementQsVar.length)]);
    console.log('AI Opponent choice', grid()[oppChoice]);
    
    placePiece(oppChoice, game.ActivePlayer.Symbol);
    return '';
}

function phaseIPlayer(event, indexPrimary) {
    if (tileIsEmpty(event.target)) {
        if(! Game.isWinningCombo(game.ActivePlayer.Symbol, grid(), indexPrimary, false)) {
            placePiece(indexPrimary, game.ActivePlayer.Symbol);
        }
        console.log('Cannot place piece there, 3-in-a-row is not allowed in phase one.');
    }
    return '';
}

function phaseIIBot() {
    console.log('Bot move');
    const [oldTile, newTile] = GetFromToSwap(myQs(game));

    console.log('Moving form', oldTile, 'to', newTile);
    [newTile.innerHTML, oldTile.innerHTML] = [game.ActivePlayer.Symbol, newTile.innerHTML];
    if(Game.causesCombo(newTile, game.ActivePlayer.Symbol, grid(), 3)) {
        game.Phase = 3;
    }
    if(game.Phase !== 3) {
        console.log('swapped turn');
        game.swapTurn();
    }
    viewUpdate();
    return '';
}

function phaseIIPlayer(event, indexPrimary) {
    console.log('Player move');
    if(game.PreviousEvent === null) { 
        game.PreviousEvent = event;
        console.log('Swap this', event.target, ' for...');
        viewUpdate();
        return '';
    }
    console.log('Attempting to swap ', game.PreviousEvent.target, event.target);
    let indexSecondary = gridIndex(game.PreviousEvent.target);
    
    let swapLegalBool = game.swapLegalCheck(event.target, game.PreviousEvent.target);
    if (! swapLegalBool) {
        console.log('Swap is illegal.');
        game.PreviousEvent = null;
        viewUpdate();
        return '';
    }
    let IllegalComboBool = Game.isIllegalCombo(game.ActivePlayer.Symbol, grid(), indexPrimary, indexSecondary, false);
    if (IllegalComboBool) {
        console.log('Swap creates illegal combo of atleast 4.');
        game.PreviousEvent = null;
        viewUpdate();
        return '';
    }

    console.log('Swapping');
    let swapTemp = grid()[indexPrimary].innerText;
    grid()[indexPrimary].innerText = game.PreviousEvent.target.innerHTML;
    grid()[indexSecondary].innerText = swapTemp;
    game.PreviousEvent = null;
    
    if(Game.causesCombo(event.target, game.ActivePlayer.Symbol, grid(), 3)){
        game.Phase = 3;
    }
    else{
        game.swapTurn();
    }
    viewUpdate();
    return '';
}


function RNG(min, max) {
    return Math.floor(Math.random() * (max - min + 1) ) + min;
}

function phaseIIIBot () {
    let oppQsArr = oppQs(game);
    console.log('oppQsArr', oppQsArr);
    let oppNoComboQsArr = oppQsArr.filter(x => ! Game.causesCombo(x, game.InActivePlayer.Symbol, grid(), 3));
    console.log('oppNoComboArr', oppNoComboQsArr);
    let tileToDestroy = oppNoComboQsArr[RNG(0, oppNoComboQsArr.length - 1)];
    console.log('removed piece', tileToDestroy);
    if (tileToDestroy === undefined) { alert('Tie'); }
    tileToDestroy.innerHTML = '';
    game.InActivePlayer.PlacedPieces -= 1;
    game.swapTurn();
    return '';
}

function phaseIIIPlayer(event, indexPrimary) {
    let targetIsOppPiece = event.target.innerHTML === game.InActivePlayer.Symbol;
            
    if (! targetIsOppPiece) {
        console.log('Piece must belong to opponent.');
        return 'Remove failed';
    }
    let targetIsPartOfCombo = Game.causesCombo(event.target, game.InActivePlayer.Symbol, grid(), 3);
    if (targetIsPartOfCombo) {
        console.log('Cannot remove combod pieces', event.target);
        return 'Remove failed';
    }
    else {
        grid()[indexPrimary].innerText = '';
        game.InActivePlayer.PlacedPieces -= 1;
        game.swapTurn();
        console.log('removed piece', event.target)
    }
    return '';
}

function shuffle(a) {
    for (let i = a.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [a[i], a[j]] = [a[j], a[i]];
    }
}

function GetFromToSwap(myQsArr) {
    shuffle(myQsArr);
    console.log('shuffled array', myQsArr);
    const boardGrid = grid();
    
    console.log('checking moves that cause combos');
    for(let i = 0; i < myQsArr.length; ++i) { 
        const oldTile = myQsArr[i];
        console.log('checking combo from', oldTile);
        const oldIndex = gridIndex(oldTile);
        const row = Number.parseInt(oldTile.classList.item(1).replace('r', ''));
        const column = Number.parseInt(oldTile.classList.item(2).replace('c', ''));
       
        oldTile.innerHTML = '';
        let newTile;

        if(column < 5 && (oldIndex + 1 < 30)) {     // right
            newTile = boardGrid[oldIndex + 1];
            if (newTile.innerHTML === '') {
                if (! Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 4)
                && Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 3)) {
                    console.log('Found combo');
                    return [oldTile, newTile];
                }  
            }
        } 
        if(column > 0 && (oldIndex - 1 > -1)) {     // left
            newTile = boardGrid[oldIndex - 1];
            if(newTile.innerHTML === '') {
                if (! Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 4)
                && Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 3)) {
                    console.log('Found combo');
                    return [oldTile, newTile];
                }
            }
        } 
        if(row < 4 && (oldIndex + 6 < 30)) {        // down
            newTile = boardGrid[oldIndex + 6];
            if(newTile.innerHTML === '') {
                if (! Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 4)
                && Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 3)) {
                    console.log('Found combo');
                    return [oldTile, newTile];
                }
            }
        } 
        if(row > 0 && (oldIndex - 6 > -1)) {        // up
            newTile = boardGrid[oldIndex - 6];
            if (newTile.innerHTML === '') {
                if (! Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 4)
                && Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 3)) {
                    console.log('Found combo');
                    return [oldTile, newTile];
                }
            }
        }
        console.log('no combo');
        oldTile.innerHTML = game.ActivePlayer.Symbol;
    }
    console.log('checking random legal moves');
    for(let i = 0; i < myQsArr.length; ++i) {
        const oldTile = myQsArr[i];
        console.log('checking random legal move from', oldTile);
        const oldIndex = gridIndex(oldTile);

        const row = Number.parseInt(oldTile.classList.item(1).replace('r', ''));
        const column = Number.parseInt(oldTile.classList.item(2).replace('c', ''));
        
        oldTile.innerHTML = '';

        let newTile = boardGrid[oldIndex + 1];
        if((column < 5 && (oldIndex + 1 < 30) && newTile.innerHTML === '')
        && ! Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 4)) {
            return [oldTile, newTile];
        } 
        newTile = boardGrid[oldIndex - 1];
        if((column > 0 && (oldIndex - 1 > -1) && newTile.innerHTML === '')
        && ! Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 4)) {
            return [oldTile, newTile];
        } 
        newTile = boardGrid[oldIndex + 6];
        if((row < 4 && (oldIndex + 6 < 30) && newTile.innerHTML === '')
        && ! Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 4)) {
            return [oldTile, newTile];
        } 
        newTile = boardGrid[oldIndex - 6];
        if((row > 0 && (oldIndex - 6 > -1) && newTile.innerHTML === '')
        && ! Game.causesCombo(newTile, game.ActivePlayer.Symbol, boardGrid, 4)) {
            return [oldTile, newTile];
        }
        oldTile.innerHTML = game.ActivePlayer.Symbol;
    }
    alert('No choice');
    return '';
}

const enableListeners = () => {
    grid().forEach(qEl => qEl.addEventListener('click', clickFn)); 
    document.getElementById('Start_btn').addEventListener('click', reset);
}
const disableListeners = () => {
    grid().forEach(qEl => qEl.removeEventListener('click', clickFn)); 
}

const gridIndex = (qEl) => Number.parseInt(qEl.id.replace('q', ''));    // get quadrent index
const emptyQs = () => grid().filter(qEl => qEl.innerText === '');     // get empty quadrents
const legalPlacementQs = () => emptyQs().filter(qEl => ! Game.isWinningCombo(game.ActivePlayer.Symbol, grid(), gridIndex(qEl), true));

const myQs = (game) => grid().filter(qEl => qEl.innerText === game.ActivePlayer.Symbol);
const oppQs = (game) => grid().filter(qEl => qEl.innerText === game.InActivePlayer.Symbol);

const tileIsEmpty = (qEl) => { return qEl.innerText === ''; }

const placePiece = (index, letter) => { 
    grid()[index].innerText = letter;
    game.swapTurnAndAddPiece();
    viewUpdate();
}

function viewUpdate() {
    if(game.Phase === 3) { document.getElementById('container2').style.backgroundColor = '#363636'; }
    else{document.getElementById('container2').style.backgroundColor = 'transparent';}

    grid().forEach(x => document.getElementById(x.id).style.backgroundColor = 'transparent');
    if(game.PreviousEvent !== null){document.getElementById(game.PreviousEvent.target.id).style.backgroundColor = '#0080ff';}
    const gameStateInfo = document.querySelector('#game_state_info');
    
    const phaseDisplay = gameStateInfo.querySelector('.phase_display');
    const blackPiecesPlacedDisplay = gameStateInfo.querySelector('.black_pieces_placed_display');
    const whitePiecesPlacedDisplay = gameStateInfo.querySelector('.white_pieces_placed_display');
    const activePlayerDisplay = gameStateInfo.querySelector('.active_player_display');
    const gameModeDisplayMsg = gameStateInfo.querySelector('.game_mode_display');
    
    phaseDisplay.innerHTML = game.getPhaseMsg();
    blackPiecesPlacedDisplay.innerHTML = game.BlackPlayer.getPiecesToPlaceMsg();
    whitePiecesPlacedDisplay.innerHTML = game.WhitePlayer.getPiecesToPlaceMsg();
    activePlayerDisplay.innerHTML = game.getActivePlayerMsg();
    gameModeDisplayMsg.innerHTML = game.getGameModeMsg();
}


const reset = () => {
    grid().forEach(qEL => qEL.innerHTML = '');
    const blackPlayerStarts = Array.from(document.getElementsByName('starting_player')).find(El => El.checked).value === "1";
    const gameMode = Array.from(document.getElementsByName('opponent')).find(El => El.checked).value;
    if (gameMode === "1") { game = new Game(false, true, blackPlayerStarts);}
    else if (gameMode === "2") { game = new Game(false, false, blackPlayerStarts);}
    else if (gameMode === "3") { game = new Game(true, true, blackPlayerStarts);}
    viewUpdate();
    enableListeners();
    console.log('Game started in mode', gameMode, game, '; black player starts:', blackPlayerStarts);
} 

reset();
