<template>
    <div v-if="getJWT != null">
        <h1>Create</h1>
        <h4>Quiz</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form onload="quiz()" submit.trigger="onSubmit($event)">

                    <div class="form-group">
                        <label class="control-label" for="Name">Name</label>
                        <input class="form-control" type="text" data-val="true" data-val-required="The Name field is required."
                         id="Name" name="Name" v-model="getQuiz.name"/>
                        <span class="text-danger field-validation-valid" data-valmsg-for="Name" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Description">Description</label>
                        <input class="form-control" type="text" data-val="true" data-val-required="The Description field is required."
                         id="Description" name="Description" v-model="getQuiz.description"/>
                        <span class="text-danger field-validation-valid" data-valmsg-for="Description" data-valmsg-replace="true"></span>
                    </div>

                    <!--
                    <div class="form-group">
                        <input type="submit" value="Create" class="btn btn-primary" />
                    </div>
                    -->
                    <button @click="createOnClick(getQuiz)" type="button" class="btn btn-primary">Create</button>
                </form>
            </div>
        </div>

        <div>
            <router-link :to="{ name: 'QuizIndex' }">Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IQuiz } from "../../domain/IQuiz";
import store from "../../store";

@Component
export default class QuizCreate extends Vue {
    get getQuiz(): IQuiz {
        const quiz : IQuiz = {
            id: "",
            name: "",
            description: "",
        };
        return quiz;
    };

    get getJWT(){
        return store.state.jwt;
    }

    createOnClick(quiz: IQuiz): void {
        console.log("Clicked on createOnClick button");
        store.dispatch('createQuiz', quiz);
        router.push({ name: 'QuizIndex' });
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
