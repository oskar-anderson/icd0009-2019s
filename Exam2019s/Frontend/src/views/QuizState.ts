import { IQuestionIndex } from '@/domain/IQuestion';
import { IChoice } from '@/domain/IChoice';

export default class QuizState {
    gameStarted: boolean;
    gameEnded: boolean;
    currentQuestionIndex: number;
    currentQuestion: IQuestionIndex | null;
    mixedOptions: IChoice[];
    trueAnswers: number;
    wrongAnswers: number;
    questionToAnswers: string;
    trueOptionValues: string[];

    questions: IQuestionIndex[];

    constructor(questions: IQuestionIndex[]){
        this.gameStarted = false;
        this.gameEnded = false;
        this.currentQuestionIndex = 0;
        this.currentQuestion = null;
        this.mixedOptions = [];
        this.trueAnswers = 0;
        this.wrongAnswers = 0;
        this.trueOptionValues = [];
        this.questionToAnswers = "";

        this.questions = questions;
    }


    startGame() {
        console.log("gameStarted")
        this.gameStarted = true;
        this.getCurrentQuestion();
    }

    getCurrentQuestion() {
        if (this.currentQuestionIndex < this.questions.length) {
            this.currentQuestion = this.questions[this.currentQuestionIndex];
            this.currentQuestionIndex++;
            this.currentQuestion.choices = this.mixedOptions;
        } else {
            this.gameEnded = true;
        }
    }

    answerCount(currentOption: IChoice) {
        currentOption.givesPoints ? this.trueAnswers++ : this.wrongAnswers++;
        this.trueOptionValues = [];
        for (const choice of this.currentQuestion!.choices) {
            this.trueOptionValues.push(choice.givesPoints ? choice.name : "")
        }
    }

    startAgain() {
        this.currentQuestionIndex = 0;
        this.trueAnswers = 0;
        this.wrongAnswers = 0;
        this.gameStarted = false;
        this.gameEnded = false;
    }

}