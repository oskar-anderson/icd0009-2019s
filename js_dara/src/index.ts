import { Game } from './GameEngine/game';
import { getElementById, getElementFromQuery, getElementsByName } from './Utils/utils';

let game: Game;

const clickFn =  (event: MouseEvent) => {
    gameController(event);
    viewUpdate();
    // turn is already swapped
    if(game.Phase === 2) {
        if(game.ActivePlayer.PlacedPieces < 3) {
            disableListeners();
            alert('Game Over! ' + game.InActivePlayer.Name + ' won!');
        }
        else {
            let availableMoves = false;
            for (const myQ of Game.myQs(game)) {
                for (const emptyQ of Game.emptyQs()) {
                    if (Game.adjacencyCheck(myQ, emptyQ)) {
                        availableMoves = true
                    }
                }
            }
            if (! availableMoves) {
                disableListeners();
                alert('Game Over! Cannot make any legal moves. Tie!');
            }
        }
    } 
}

const gameController = (event: MouseEvent) => {
    const indexPrimary = Game.gridIndex(<HTMLElement> event.target);
    console.clear();
    console.log('grid', Game.grid());
    // console.dir(grid()[0]);
    console.log('Game', game);
    console.log('Active player', game.ActivePlayer, 'Inactive player' ,game.InActivePlayer);
    console.log();
    console.log();
    console.log('User clicked on', event.target);

    if (game.Phase === 1) {
        console.log('Phase 1');
        if(! game.ActivePlayer.IsAiPlayer && ! game.InActivePlayer.IsAiPlayer) {
            console.log('Player vs Player');
            game.phaseIPlayer(event, indexPrimary);
            return null;
        }
        else if(game.ActivePlayer.IsAiPlayer && game.InActivePlayer.IsAiPlayer) {
            console.log('Ai vs Ai');
            game.phaseIBot();
            return null;
        }
        else {
            console.log('Player vs AI');
            if(game.ActivePlayer.IsAiPlayer){
                game.phaseIBot();
                return null;
            }
            else {
                game.phaseIPlayer(event, indexPrimary);
                return null;
            }
        }
    } if (game.Phase === 2) {
        console.log('Phase 2');

        if ((! game.ActivePlayer.IsAiPlayer && ! game.InActivePlayer.IsAiPlayer)) {
            console.log('Player VS Player');
            game.phaseIIPlayer(event, indexPrimary);
            return null;
        }
        else if(game.ActivePlayer.IsAiPlayer && game.InActivePlayer.IsAiPlayer){
            console.log('AI VS AI');
            game.phaseIIBot();
            return null;
        }
        else {
            console.log('Player vs AI');
            if(game.ActivePlayer.IsAiPlayer){
                game.phaseIIBot();
                return null;
            } else {
                game.phaseIIPlayer(event, indexPrimary);
                return null;
            }
        }
    } if (game.Phase === 3) {
        console.log('Phase 3');
        console.log('Active player ' + game.ActivePlayer.Symbol);
        if(game.ActivePlayer.IsAiPlayer){
            game.phaseIIIBot();
        }
        else if(game.phaseIIIPlayer(event, indexPrimary) === 'Remove failed') {
            return null; 
        }
        game.Phase = 2;
    }
}

const enableListeners = () => {
    Game.grid().forEach(qEl => qEl.addEventListener('click', clickFn)); 
    getElementById('Start_btn').addEventListener('click', reset);
}
const disableListeners = () => {
    Game.grid().forEach(qEl => qEl.removeEventListener('click', clickFn)); 
}

function viewUpdate() {
    if(game.Phase === 3) { document.getElementById('container2')!.style.backgroundColor = '#363636'; }
    else{getElementById('container2').style.backgroundColor = 'transparent';}

    Game.grid().forEach(x => document.getElementById(x.id)!.style.backgroundColor = 'transparent');
    if(game.PreviousEvent !== null) {
        getElementById((<HTMLElement> game.PreviousEvent.target).id).style.backgroundColor = '#0080ff';
    }

    const gameStateInfo = getElementFromQuery(document, '#game_state_info');
    
    const phaseDisplay = getElementFromQuery(gameStateInfo, '.phase_display');
    const blackPiecesPlacedDisplay = getElementFromQuery(gameStateInfo, '.black_pieces_placed_display');
    const whitePiecesPlacedDisplay = getElementFromQuery(gameStateInfo, '.white_pieces_placed_display');
    const activePlayerDisplay = getElementFromQuery(gameStateInfo, '.active_player_display');
    const gameModeDisplayMsg = getElementFromQuery(gameStateInfo, '.game_mode_display');
    
    phaseDisplay.innerHTML = game.getPhaseMsg();
    blackPiecesPlacedDisplay.innerHTML = game.BlackPlayer.getPiecesToPlaceMsg();
    whitePiecesPlacedDisplay.innerHTML = game.WhitePlayer.getPiecesToPlaceMsg();
    activePlayerDisplay.innerHTML = game.getActivePlayerMsg();
    gameModeDisplayMsg.innerHTML = game.getGameModeMsg();
}

const reset = () => {
    Game.grid().forEach(qEL => qEL.innerHTML = '');

    const blackPlayerStarts = (<HTMLInputElement> getElementsByName('starting_player').find(El => El.checked)).value === "1";
    const gameMode = (<HTMLInputElement> getElementsByName('opponent').find(El => El.checked)).value;
    if (gameMode === "1") { game = new Game(false, true, blackPlayerStarts);}
    else if (gameMode === "2") { game = new Game(false, false, blackPlayerStarts);}
    else if (gameMode === "3") { game = new Game(true, true, blackPlayerStarts);}
    viewUpdate();
    enableListeners();
    console.log('Game started in mode', gameMode, game, '; black player starts:', blackPlayerStarts);
} 

reset();
