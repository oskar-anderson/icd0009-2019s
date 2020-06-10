<template>
    <div v-if="getJWT != null">
        <h1>Create</h1>
        <h4>Choice</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form onload="choice()" submit.trigger="onSubmit($event)">

                    <div class="form-group">
                        <label class="control-label" for="QuestionId">QuestionId</label>
                        <select class="form-control" data-val="true" data-val-required="The QuestionId field is required."
                         id="QuestionId" name="QuestionId" v-model="getChoice.questionId">
                            <option
                                v-for="question in getQuestions"
                                :key="question.id"
                                v-bind:value="question.id">
                                {{ question.questionName }}
                            </option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Name">Name</label>
                        <input class="form-control" type="text" data-val="true" data-val-required="The Name field is required."
                         id="Name" name="Name" v-model="getChoice.name" />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="GivesPoints">GivesPoints</label>
                        <input style="width: inherit;" class="form-control" type="checkbox" checked="checked" data-val="true" 
                        data-val-required="The GivesPoints field is required." id="GivesPoints" name="GivesPoints"
                        v-model="getChoice.givesPoints" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="GivesPoints" data-valmsg-replace="true"></span>
                    </div>
                    <!--
                    <div class="form-group">
                        <input type="submit" value="Create" class="btn btn-primary" />
                    </div>
                    -->
                    <button @click="createOnClick(getChoice)" type="button" class="btn btn-primary">Create</button>
                </form>
            </div>
        </div>

        <div>
            <router-link :to="{ name: 'ChoiceIndex' }">Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IChoice } from "../../domain/IChoice";
import { IQuestion } from "../../domain/IQuestion";
import store from "../../store";

@Component
export default class ChoiceCreate extends Vue {
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

    createOnClick(choice: IChoice): void {
        console.log("Clicked on createOnClick button");
        store.dispatch('createChoice', choice);
        router.push({ name: 'ChoiceIndex' });
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
