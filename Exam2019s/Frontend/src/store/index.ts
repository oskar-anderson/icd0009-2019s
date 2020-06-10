import Vue from 'vue'
import Vuex from 'vuex'

import { ILoginDTO } from '@/types/ILoginDTO'
import { AccountApi } from '@/services/AccountApi'
import { IRegisterDTO } from '@/types/IRegisterDTO';

import { IChoice } from './../domain/IChoice';
import { ChoiceApi } from '@/services/ChoiceApi';

import { IQuestion } from './../domain/IQuestion';
import { QuestionApi } from '@/services/QuestionApi';

import { IQuiz } from '../domain/IQuiz';
import { QuizApi } from '@/services/QuizApi';

import { IResult } from './../domain/IResult';
import { ResultApi } from '@/services/ResultApi';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        jwt: null as string | null,
        
        choices: [] as IChoice[],
        choice: null as IChoice | null,
        
        questions: [] as IQuestion[],
        question: null as IQuestion | null,

        quizzes: [] as IQuiz[],
        quiz: null as IQuiz | null,

        results: [] as IResult[],
        result: null as IResult | null,
    },
    mutations: {
        setJwt(state, jwt: string | null) {
            state.jwt = jwt
        },


        setChoices(state, choices: IChoice[]) {
            state.choices = choices;
        },
        setChoice(state, choice: IChoice) {
            state.choice = choice;
        },


        setQuizzes(state, quizzes: IQuiz[]) {
            state.quizzes = quizzes;
        },
        setQuiz(state, quiz: IQuiz) {
            state.quiz = quiz;
        },


        setQuestions(state, questions: IQuestion[]) {
            state.questions = questions;
        },
        setQuestion(state, question: IQuestion) {
            state.question = question;
        },


        setResults(state, result: IResult[]) {
            state.results = result;
        },
        setResult(state, result: IResult) {
            state.result = result;
        }
    },
    getters: {
        isAuthenticated(context): boolean {
            return context.jwt !== null;
        }
    },
    actions: {
        clearJwt(context): void{
            context.commit('setJwt', null);
        },
        async authenticateUser(context, loginDTO: ILoginDTO): Promise<boolean> {
            const jwt = await AccountApi.getJwt(loginDTO);
            context.commit('setJwt', jwt);
            return jwt != null;
        },
        async registerUser(context, registerDTO: IRegisterDTO): Promise<boolean> {
            await AccountApi.register(registerDTO);
            
            
            const jwt = await AccountApi.getJwt(registerDTO);
            context.commit('setJwt', jwt);
            return jwt != null;
        },
        
        // Choices

        async getChoices(context): Promise<void> {
            const choices = await ChoiceApi.getAll();
            context.commit('setChoices', choices);
        },
        async getChoice(context, id: string): Promise<void> {
            if (!id) {
                context.commit('setChoice', null);
            } else {
                const choice = await ChoiceApi.get(id);
                context.commit('setChoice', choice);
            }
        },
        async deleteChoice(context, id: string): Promise<void> {
            console.log('isAuthenticated for deleteChoice', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await ChoiceApi.delete(id, context.state.jwt);
                await context.dispatch('getChoices');
            } else {
                alert("Log in!");
            }
        },
        async editChoice(context, choice: IChoice): Promise<void> {
            console.log('isAuthenticated for editChoice', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await ChoiceApi.edit(choice, context.state.jwt);
                await context.dispatch('getChoices');
            } else {
                alert("Log in!");
            }
        },
        async createChoice(context, choice: IChoice): Promise<void> {
            console.log('isAuthenticated for createChoice', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await ChoiceApi.create(choice, context.state.jwt);
                await context.dispatch('getChoices');
            } else {
                alert("Log in!");
            }
        },

        // Quizzes

        async getQuizzes(context): Promise<void> {
            const quizzes = await QuizApi.getAll();
            context.commit('setQuizzes', quizzes);
        },
        async getQuiz(context, id: string): Promise<void> {
            if (!id) {
                context.commit('setQuiz', null);
            } else {
                const quiz = await QuizApi.get(id);
                context.commit('setQuiz', quiz);
            }
        },
        async deleteQuiz(context, id: string): Promise<void> {
            console.log('isAuthenticated for deleteQuiz', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await QuizApi.delete(id, context.state.jwt);
                await context.dispatch('getQuizzes');
            } else {
                alert("Log in!");
            }
        },
        async editQuiz(context, quiz: IQuiz): Promise<void> {
            console.log('isAuthenticated for editQuiz', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await QuizApi.edit(quiz, context.state.jwt);
                await context.dispatch('getQuizzes');
            } else {
                alert("Log in!");
            }
        },
        async createQuiz(context, quiz: IQuiz): Promise<void> {
            console.log('isAuthenticated for createQuiz', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await QuizApi.create(quiz, context.state.jwt);
                await context.dispatch('getQuizzes');
            } else {
                alert("Log in!");
            }
        },

        // Questions

        async getQuestions(context): Promise<void> {
            const questions = await QuestionApi.getAll();
            context.commit('setQuestions', questions);
        },
        async getQuestion(context, id: string): Promise<void> {
            if (!id) {
                context.commit('setQuestion', null);
            } else {
                const question = await QuestionApi.get(id);
                context.commit('setQuestion', question);
            }
        },
        async deleteQuestion(context, id: string): Promise<void> {
            console.log('isAuthenticated for deleteQuestion', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await QuestionApi.delete(id, context.state.jwt);
                await context.dispatch('getQuestions');
            } else {
                alert("Log in!");
            }
        },
        async editQuestion(context, question: IQuestion): Promise<void> {
            console.log('isAuthenticated for editQuestion', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await QuestionApi.edit(question, context.state.jwt);
                await context.dispatch('getQuestions');
            } else {
                alert("Log in!");
            }
        },
        async createQuestion(context, question: IQuestion): Promise<void> {
            console.log('isAuthenticated for createQuestion', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await QuestionApi.create(question, context.state.jwt);
                await context.dispatch('getQuestions');
            } else {
                alert("Log in!");
            }
        },

        // Results

        async getResults(context): Promise<void> {
            const results = await ResultApi.getAll();
            context.commit('setResults', results);
        },
        async getResult(context, id: string): Promise<void> {
            if (!id) {
                context.commit('setResult', null);
            } else {
                const result = await ResultApi.get(id);
                context.commit('setResult', result);
            }
        },
        async deleteResult(context, id: string): Promise<void> {
            console.log('isAuthenticated for deleteResult', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await ResultApi.delete(id, context.state.jwt);
                await context.dispatch('getResult');
            } else {
                alert("Log in!");
            }
        },
        async editResult(context, result: IResult): Promise<void> {
            console.log('isAuthenticated for editResult', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await ResultApi.edit(result, context.state.jwt);
                await context.dispatch('getResults');
            } else {
                alert("Log in!");
            }
        },
        async createResult(context, result: IResult): Promise<void> {
            console.log('isAuthenticated for createResult', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await ResultApi.create(result, context.state.jwt);
                await context.dispatch('getResults');
            } else {
                alert("Log in!");
            }
        },



    },
    modules: {
    }
})
