import { IChoice } from './IChoice';

export interface IQuestion {
    id: string;
    quizId: string,
    orderNumber: number;
    questionName: string;
    points: number;
}

export interface IQuestionIndex {
    id: string;
    quizId: string,
    orderNumber: number;
    questionName: string;
    points: number;
    choices: IChoice[]
    newChoiceName: string;
    newChoiceGivesPoints: boolean;
    selectedChoice: IChoice | null;
}
