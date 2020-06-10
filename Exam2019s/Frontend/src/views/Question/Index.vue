<template>
    <div v-if="getJWT != null">
        <h1>Index Questions</h1>

        <p>
            <router-link to="Question/Create">Create new</router-link>
        </p>

        <table class="table">
            <thead>
                <tr>
                    <th>
                        Quiz
                    </th>
                    <th>
                        #
                    </th>
                    <th>
                        Question
                    </th>
                    <th>
                        Points
                    </th>
                    <th>
                        Choices
                    </th>

                    <th>
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="question in getQuestionsWithChoices" :key="question.id">
                    <td>
                        {{question.quizId}}
                    </td>
                    <td>
                        {{question.orderNumber}}
                    </td>
                    <td>
                        {{question.questionName}}
                    </td>
                    <td>
                        {{question.points}}
                    </td>
                    <td>

                        <span v-if="question.choices.length !== 0">
                            <div style="height: 28px" v-for="choice in question.choices" :key="choice.id">
                                {{choice.name}}&nbsp;-&nbsp;{{choice.givesPoints? "Õige" : "Vale"}}
                                <i style="float: right;" @click="deleteChoice(choice)" class="fa fa-color-a fa-2x fa-trash"></i>
                                <br>
                            </div>
                            <hr>
                        </span>
                        <table>
                            <span v-if="question.choices.length === 0">
                                <b>Question doesn't have choices</b>
                            </span>
                            <span v-if="question.choices.length !== 0">
                                <b>New</b>
                            </span>

                            <tr>
                            <td style="border-top: 0; padding: 0" class="form-group">
                                <label class="control-label" for="Name">Name</label>
                                <textarea class="form-control" type="text" data-val="true" data-val-required="The Name field is required."
                                id="Name" name="Name" v-model="question.newChoiceName" />
                            </td>
                            
                            <td style="border-top: 0; padding-top: 0" class="form-group">
                                <label class="control-label" for="GivesPoints">Gives Points</label>
                                <input style="width: inherit;" class="form-control" type="checkbox" data-val="true" 
                                data-val-required="The GivesPoints field is required." id="GivesPoints" name="GivesPoints"
                                v-model="question.newChoiceGivesPoints" />
                                <span class="text-danger field-validation-valid" data-valmsg-for="GivesPoints" data-valmsg-replace="true"></span>
                            </td>
                            </tr>
                            <button @click="choiceSubmit($event, question)" type="button" class="btn btn-primary">Create</button>
                        </table>
                    </td>

                    <td>
                        <router-link :to="{ name: 'QuestionEdit', params: { id: question.id }}">Edit</router-link>
                        <button @click="onDelete(question)" type="button" class="btn btn-danger">Delete</button>
                    </td>

                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IQuestion, IQuestionIndex } from "../../domain/IQuestion";
import { IChoice } from "../../domain/IChoice";
import store from "../../store";

@Component
export default class QuestionIndex extends Vue {
    get getChoice(): IChoice {
        const choice : IChoice = {
            id: "",
            questionId: "",
            name: "",
            givesPoints: false,
        };
        return choice;
    };

    get getJWT(){
        return store.state.jwt;
    }

    get getQuestions(): IQuestion[] {
        return store.state.questions;
    }

    get getChoices(): IChoice[] {
        return store.state.choices;
    }

    deleteChoice(choice: IChoice) {
        console.log("clicked on delete button!")
        store.dispatch('deleteChoice', choice.id)
        router.push({ name: 'QuestionIndex' });
    }

    choiceSubmit(event: Event, question: IQuestionIndex){
        console.log("Clicked on createOnClick button");
        console.log(question.newChoiceName);
        let newChoice : IChoice = {
            id: "",
            name: question.newChoiceName,
            givesPoints: question.newChoiceGivesPoints,
            questionId: question.id,
        }
        store.dispatch('createChoice', newChoice);
        router.push({ name: 'QuestionIndex' });
    }

    get getQuestionsWithChoices(): IQuestionIndex[]{
        let oldQuestions = store.state.questions;
        let choices = store.state.choices;
        let newQuestions = [];
        for (const question of oldQuestions) {
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
                selectedChoice: null
            };
            newQuestions.push(questionIndex);
        }
        return newQuestions;
    }

    onDelete(question: IQuestion): void {
        console.log("clicked on delete button!")
        console.log(question);
        store.dispatch('deleteQuestion', question.id)
        router.push({ name: 'QuestionIndex' });
    };

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
        store.dispatch("getChoices")
        store.dispatch("getQuestions");
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
