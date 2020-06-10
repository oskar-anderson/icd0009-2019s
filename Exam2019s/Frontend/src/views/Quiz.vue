<template>
    <div>
        <div class="container d-flex h-100 align-items-center">
            <div class="mx-auto text-center">
                <h1 class="mx-auto my-0 text-uppercase">{{getQuizData.name}}</h1>
                <h2 class="mx-auto mt-2 mb-5">{{getQuizData.description}}</h2>
            </div>
        </div>
        <div v-for="(question, index) in getQuizData.questionsIndex" :key="index" style="padding-bottom: 1rem">
            {{index + 1}}. {{question.points}}p - {{question.questionName}} 
            <div v-for="choice in question.choices" :key="choice.id">
                <div class="container" >
                    <input type="radio" :value="choice" v-model="question.selectedChoice" :name="question.id">{{choice.name}}
                </div>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label" for="PersonName">Name</label>
            <input class="form-control" type="text" data-val="true" data-val-required="The PersonName field is required." 
            id="PersonName" name="PersonName" v-model="getResult.personName"/>
            <span class="text-danger field-validation-valid" data-valmsg-for="PersonName" data-valmsg-replace="true"></span>
        </div>
        <button @click="createOnClick(getQuizData, getResult)" type="button" class="btn btn-primary">Submit</button>

    </div>
</template>

<script lang="ts">

import router from '../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IQuiz, IQuizIndex } from "../domain/IQuiz";
import { IChoice } from "../domain/IChoice";
import { IResult } from "../domain/IResult";
import { IQuestion, IQuestionIndex } from "../domain/IQuestion";
import store from "../store";
import QuizState from "./QuizState";

@Component
export default class QuizIndex extends Vue {
    get getQuizzes(): IQuiz[] {
        return store.state.quizzes;
    }
    get getQuiz(): IQuiz {
        return store.state.quiz!;
    }
    get getQuestions(): IQuestion[] {
        return store.state.questions;
    }
    get getChoices(): IChoice[] {
        return store.state.choices;
    }

    get getResult(): IResult {
        const result : IResult = {
            id: "",
            quizId: "",
            score: 0,
            questionToPickedAnswer: "",
            personName: "",
        };
        return result;
    };

    get getQuizData(): IQuizIndex {
        let oldQuiz = store.state.quizzes.find(x => x.id === this.$route.params.id);
        if (oldQuiz === undefined) {
            alert("Quiz not found!")
            router.push({ name: '/' });
        }
        let choices: IChoice[] = store.state.choices;
        let questionsTemp = store.state.questions.filter(x => x.quizId === this.$route.params.id);
        let questions = [];
        for (const question of questionsTemp) {
            let children = choices.filter(x => x.questionId === question.id);
            let questionIndex : IQuestionIndex = {
                id: question.id,
                quizId: question.quizId,
                orderNumber: question.orderNumber,
                questionName: question.questionName,
                points: question.points,
                choices: [...children],
                newChoiceName: "",
                newChoiceGivesPoints: false,
                selectedChoice: null,
            };
            questions.push(questionIndex);
        }


        let quiz : IQuizIndex = {
            id: oldQuiz!.id,
            name: oldQuiz!.name,
            description: oldQuiz!.description,
            questions: questions,
            questionsIndex: questions,
            results: [],
            avg: 0,
            totalAttempts: 0,
            instance: new QuizState(questions)
        }
        return quiz;
    }

    createOnClick(quizDate: IQuizIndex, result: IResult){
        console.log(result.personName);
        let score = 0;
        let questionToAnswer = "";
        let questionNumber = 0;
        for (let question of quizDate.questionsIndex!) {
            questionNumber++
            if (question.selectedChoice == null) {
                console.log("Answer for " + question.questionName + " was not selected.");
                questionToAnswer += questionNumber + ". question for " + question.points + " - " + question.questionName + " - " + "Unanswered" + ";\n"
            }
            else {
                if (question.selectedChoice.givesPoints) {
                    console.log(questionNumber + ". Answer was correct!");
                    score += question.points;
                } else {
                    console.log(questionNumber + ". Answer was incorrect!");
                }
                questionToAnswer += questionNumber + ". question for " + question.points + "p - " + question.questionName + " - " + question.selectedChoice.name + ";\n"
            }
        }
        console.log(score);
        console.log(questionToAnswer);
        let endResult: IResult = {
            id: "",
            quizId: quizDate.id,
            score: score,
            questionToPickedAnswer: questionToAnswer,
            personName: result.personName === "Anon" ? "" : result.personName
        } 
        store.dispatch('createResult', endResult);
        router.push({ name: '/' });
    }



    // ============ Lifecycle methods ==========
    beforeCreate(): void {
        console.log("beforeCreate");
    }

    created(): void {
        console.log("created");
    }

    beforeMount(): void {
        console.log("beforeMount");
    }

    mounted(): void {
        store.dispatch("getQuiz", this.$route.params.id)
        store.dispatch("getQuestions");
        store.dispatch("getChoices");
        console.log("mounted");
    }

    beforeUpdate(): void {
        console.log("beforeUpdate");
    }

    updated(): void {
        console.log("updated");
    }

    beforeDestroy(): void {
        console.log("beforeDestroy");
    }

    destroyed(): void {
        console.log("destroyed");
    }
}

</script>

<style scoped>
.masthead {
  position: relative;
  width: 100%;
  height: auto;
  min-height: 35rem;
  padding: 15rem 0;
  background: linear-gradient(
      to bottom,
      rgba(22, 22, 22, 0.3) 0%,
      rgba(22, 22, 22, 0.7) 75%,
      #161616 100%
    );
}

</style>