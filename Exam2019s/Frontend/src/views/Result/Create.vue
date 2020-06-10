<template>
    <div v-if="getJWT != null">
        <h1>Create</h1>
        <h4>Result</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form onload="result()" submit.trigger="onSubmit($event)">

                    <div class="form-group">
                        <label class="control-label" for="QuizId">QuizId</label>
                        <select class="form-control" data-val="true" data-val-required="The QuizId field is required."
                         id="QuizId" name="QuizId"  v-model="getResult.quizId">
                            <option
                                v-for="quiz in getQuizzes"
                                :key="quiz.id"
                                v-bind:value="quiz.id">
                                {{ quiz.name }}
                            </option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Score">Score</label>
                        <input class="form-control" type="number" data-val="true" data-val-required="The Score field is required." 
                        id="Score" name="Score" v-model="getResult.score" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="Score" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="QuestionToPickedAnswer">QuestionToPickedAnswer</label>
                        <input class="form-control" type="text" data-val="true" data-val-required="The QuestionToPickedAnswer field is required." 
                        id="QuestionToPickedAnswer" name="QuestionToPickedAnswer" v-model="getResult.questionToPickedAnswer" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="QuestionToPickedAnswer" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="PersonName">PersonName</label>
                        <input class="form-control" type="text" data-val="true" data-val-required="The PersonName field is required." 
                        id="PersonName" name="PersonName" v-model="getResult.personName"/>
                        <span class="text-danger field-validation-valid" data-valmsg-for="PersonName" data-valmsg-replace="true"></span>
                    </div>
                    <!--
                    <div class="form-group">
                        <input type="submit" value="Create" class="btn btn-primary" />
                    </div>
                    -->
                    <button @click="createOnClick(getResult)" type="button" class="btn btn-primary">Create</button>
                </form>
            </div>
        </div>

        <div>
            <router-link :to="{ name: 'ResultIndex' }">Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IResult } from "../../domain/IResult";
import { IQuiz } from "../../domain/IQuiz";
import store from "../../store";

@Component
export default class ResultCreate extends Vue {
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

    get getJWT(){
        return store.state.jwt;
    }

    get getQuizzes(): IQuiz[] {
        return store.state.quizzes;
    }

    createOnClick(result: IResult): void {
        console.log("Clicked on createOnClick button");
        store.dispatch('createResult', result);
        router.push({ name: 'ResultIndex' });
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
        store.dispatch("getQuizzes");
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
