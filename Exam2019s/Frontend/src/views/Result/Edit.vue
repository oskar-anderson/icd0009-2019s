<template>
    <div v-if="getJWT != null">
        <h1>Edit</h1>

        <h4>Result</h4>
        <hr>
        <div class="row">
            <div class="col-md-4">
                <form submit.trigger="onSubmit($event)">

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
                        <input type="submit" value="Submit" class="btn btn-primary" />
                    </div>
                    -->
                    <button @click="updateOnClick(getResult)" type="button" class="btn btn-primary">Submit</button>
                </form>
            </div>
        </div>

        <div>
            <router-link :to="{ name: 'ResultIndex' }">Back to list</router-link>
        </div>

    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IQuiz } from "../../domain/IQuiz";
import { IResult } from "../../domain/IResult";
import store from "../../store";
import router from '../../router';

@Component
export default class ResultEdit extends Vue {
    get getResult(): IResult {
        const result = store.state.result;
        const resultNotFound : IResult = {
            id: "",
            quizId: "",
            score: 0,
            questionToPickedAnswer: "",
            personName: "",
        };
        return result != null ? result : resultNotFound;
    };

    get getJWT(){
        return store.state.jwt;
    }

    get getQuizzes(): IQuiz[] {
        return store.state.quizzes;
    };

    updateOnClick(result: IResult): void {
        console.log("Clicked on updateOnClick button");
        store.dispatch('editResult', result);
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
        store.dispatch("getResult", this.$route.params.id)
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
