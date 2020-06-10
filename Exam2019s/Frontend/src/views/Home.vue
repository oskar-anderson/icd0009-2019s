<template>
    <div>
        <div v-for="quiz in getQuestionsWithChoices" :key="quiz.id" style="margin-bottom: 3rem;">
            <div class="mx-auto text-center">
                <h1 class="mx-auto my-0 text-uppercase">
                    <router-link :to="{ name: 'Quiz', params: { id: quiz.id }}">
                        {{quiz.name}}
                    </router-link>
                </h1>
                <h4 class="mx-auto mt-2 mb-5">
                    {{quiz.description}}
                </h4>
                Times Taken: {{quiz.totalAttempts}}
                <br>
                Average Score: {{quiz.avg}}
                <br>
                Questions: {{quiz.questions.length}}
                <br>
            </div>
        </div>

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
    get getResults(): IResult[] {
        return store.state.results;
    }
    get getChoices(): IChoice[] {
        return store.state.choices;
    }



    get getQuestionsWithChoices(): IQuizIndex[]{
        let oldQuizzes = store.state.quizzes;
        let results = store.state.results;
        let newResults = [];
        for (const oldQuiz of oldQuizzes) {
            let children = results.filter(x => x.quizId === oldQuiz.id);
            
            var sum = 0;
            for( var i = 0; i < children.length; i++ ){
                sum += parseInt(children[i].score + "", 10 ); //don't forget to add the base
            }
            var avg = sum / children.length;
            if (children.length === 0) {
                avg = 0;
            }

            let questionIndex : IQuizIndex = {
                id: oldQuiz.id,
                name: oldQuiz.name,
                description: oldQuiz.description,
                questions: store.state.questions.filter(x => x.quizId === oldQuiz.id),
                questionsIndex: null,
                results: [...children],
                avg: avg,
                totalAttempts: children.length,
                instance: null
            };
            newResults.push(questionIndex);
        }
        return newResults;
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
        store.dispatch("getQuestions")
        store.dispatch("getQuizzes")
        store.dispatch("getResults");
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