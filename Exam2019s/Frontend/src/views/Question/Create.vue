<template>
    <div v-if="getJWT != null">
        <h1>Create</h1>
        <h4>Question</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form onload="question()" submit.trigger="onSubmit($event)">

                    <div class="form-group">
                        <label class="control-label" for="QuizId">QuizId</label>
                        <select class="form-control" data-val="true" data-val-required="The QuizId field is required."
                         id="QuestionId" name="QuestionId" v-model="getQuestion.quizId">
                            <option
                                v-for="quiz in getQuizzes"
                                :key="quiz.id"
                                v-bind:value="quiz.id">
                                {{ quiz.name }}
                            </option>
                        </select>
                    </div>

                    <div class="form-group">
                        <label class="control-label" for="OrderNumber">OrderNumber</label>
                        <input class="form-control" type="number" data-val="true" data-val-required="The OrderNumber field is required."
                         id="OrderNumber" name="OrderNumber" v-model="getQuestion.orderNumber" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="OrderNumber" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="QuestionName">QuestionName</label>
                        <input class="form-control" type="text" data-val="true" data-val-required="The QuestionName field is required." 
                        id="QuestionName" name="QuestionName" v-model="getQuestion.questionName" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="QuestionName" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Points">Points</label>
                        <input class="form-control" type="number" data-val="true" data-val-required="The Points field is required." 
                        id="Points" name="Points" v-model="getQuestion.points" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="Points" data-valmsg-replace="true"></span>
                    </div>
                    <!--
                    <div class="form-group">
                        <input type="submit" value="Create" class="btn btn-primary" />
                    </div>
                    -->
                    <button @click="createOnClick(getQuestion)" type="button" class="btn btn-primary">Create</button>
                </form>
            </div>
        </div>

        <div>
            <router-link :to="{ name: 'QuestionIndex' }">Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IQuestion } from "../../domain/IQuestion";
import { IQuiz } from "../../domain/IQuiz";
import store from "../../store";

@Component
export default class QuestionCreate extends Vue {
    get getQuestion(): IQuestion {
        const question : IQuestion = {
            id: "",
            quizId: "",
            orderNumber: 1,
            questionName: "",
            points: 0,
        };
        return question;
    };

    get getJWT(){
        return store.state.jwt;
    }

    get getQuizzes(): IQuiz[] {
        return store.state.quizzes;
    }

    createOnClick(question: IQuestion): void {
        console.log("Clicked on createOnClick button");
        let questionIntified : IQuestion = {
            id: question.id,
            quizId: question.quizId,
            orderNumber: parseInt(question.orderNumber + ""),
            questionName: question.questionName,
            points: parseInt(question.points + ""),
        }
        store.dispatch('createQuestion', questionIntified);
        router.push({ name: 'QuestionIndex' });
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
