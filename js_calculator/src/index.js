'use strict'; 
import { Calculator } from './calculator.js';
import { Game } from './GameEngine/game.js';

let calcBrain = new Calculator();

let calculator = document.querySelector('#calculator');
let display = calculator.querySelector('.js-display');
let numberButtons = calculator.querySelectorAll('.js-number');

// set the initial display value
display.innerHTML = calcBrain.display;

function numberPressed(event){
    let key = this.dataset.value;
    display.innerHTML = calcBrain.handleKey(key);
} 

for (const numberButton of numberButtons) {
    numberButton.onclick = numberPressed;
}



let game = new Game(false, false, true);
const grid = () => Array.from(document.getElementsByClassName('q'));    // get all quadrents

const clickFn = (event) => {
    console.log(event.target);
    
    if (game.Phase === 1) {
        takeTurn(gridIndex(event.target), game.ActivePlayer.Symbol);       // red is X.
        opponentTurn();
        
    } else if (game.Phase === 2) {
        // todo
    } else if (game.Phase === 3) {
        // todo
        if(checkForVictory()) {
            disableListeners();
        }
        
    }
}


const enableListeners = () => grid().forEach(_qEl => _qEl.addEventListener('click', clickFn)); 
const disableListeners = () => grid().forEach(_qEl => _qEl.removeEventListener('click', clickFn)); 

enableListeners();

const gridIndex = (qEl) => Number.parseInt(qEl.id.replace('q', ''));    // get quadrent index
const emptyQs = () => grid().filter(_qEl => _qEl.innerText === '');     // get empty quadrents
const allItemsInArrAreSame = (arr) => arr.every(_qEl =>
    _qEl.innerText === arr[0].innerText && _qEl.innerText !== '');      // checks for win, false on empty

const takeTurn = (index, letter) => { 
    grid()[index].innerText = letter;
    game.swapTurn();
    view();

}

const opponentChoice = () => gridIndex(emptyQs()[Math.floor(Math.random() * emptyQs().length)]);

const endGame = () => { console.log('Game Over!'); }
const checkForVictory = () => {
    let victory = false;
    
    // todo check for win

    return victory;
}

const opponentTurn = () => {
    disableListeners();
    setTimeout(() => {
        takeTurn(opponentChoice(), 'O');
        enableListeners();
    }, 1000);
}





let gameStateInfo = document.querySelector('#game_state_info');
let phaseDisplay = gameStateInfo.querySelector('.phase_display');
let activePlayerDisplay = gameStateInfo.querySelector('.active_player_display');
let redPiecesPlacedDisplay = gameStateInfo.querySelector('.red_pieces_placed_display');
let yelPiecesPlacedDisplay = gameStateInfo.querySelector('.yel_pieces_placed_display');
let victory_msg = gameStateInfo.querySelector('.victory_msg');

function view() {
    phaseDisplay.innerHTML = game.getPhaseMsg();
    activePlayerDisplay.innerHTML = game.getActivePlayerMsg();
    redPiecesPlacedDisplay.innerHTML = game.RedPlayer.getPiecesToPlaceMsg();
    yelPiecesPlacedDisplay.innerHTML = game.YelPlayer.getPiecesToPlaceMsg();
    victory_msg.innerHTML = game.getVictoryMsg();
}

view();


window.onload = () => {
    tableCreate(game.Board);
}

function tableCreate(gameData){
    const tableBody = document.getElementById('tableData');
    let dataHtml = '';

    for (let row of gameData) {
        dataHtml += '<tr>';
        for (let cell of row) {
            dataHtml += '<td><div><a>' + 
                cell.piece + 
                '<input type="checkbox" name="cell_interaction_checkbox" value="' + 
                cell.swappable + '"></input>' + 
                '</a></div></td>';
        }
        dataHtml += '</tr>';
    }

    tableBody.innerHTML = dataHtml;
}
