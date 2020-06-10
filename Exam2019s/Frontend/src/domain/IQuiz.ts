import { IQuestion, IQuestionIndex } from './IQuestion';
import { IResult } from './IResult';
import QuizState from '@/views/QuizState';

export interface IQuiz {
    id: string;
    name: string;
    description: string;
}

export interface IQuizIndex {
    id: string;
    name: string;
    description: string;
    questions: IQuestion[] | null;
    questionsIndex: IQuestionIndex[] | null;
    results: IResult[];

    avg: number;
    totalAttempts: number;
    instance: QuizState | null;
}
