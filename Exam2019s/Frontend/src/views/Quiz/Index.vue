<template>
    <div v-if="getJWT != null">
        <h1>Index Quizzes</h1>

        <p>
            <router-link to="Quiz/Create">Create new</router-link>
        </p>

        <table class="table">
            <thead>
                <tr>
                    <th>
                        Author
                    </th>
                    <th>
                        Name
                    </th>
                    <th>
                        Description
                    </th>

                    <th>
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="quiz in getQuizzes" :key="quiz.id">
                    <td>
                        {{quiz.appUserId}}
                    </td>
                    <td>
                        {{quiz.name}}
                    </td>
                    <td>
                        {{quiz.description}}
                    </td>
                    <td>
                        <router-link :to="{ name: 'QuizEdit', params: { id: quiz.id }}">Edit</router-link>
                        <button @click="onDelete(quiz)" type="button" class="btn btn-danger">Delete</button>
                    </td>

                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IQuiz } from "../../domain/IQuiz";
import store from "../../store";

@Component
export default class QuizIndex extends Vue {
    get getQuizzes(): IQuiz[] {
        return store.state.quizzes;
    }

    get getJWT(){
        return store.state.jwt;
    }

    onDelete(quiz: IQuiz): void {
        console.log("clicked on delete button!")
        store.dispatch('deleteQuiz', quiz.id)
        router.push({ name: 'QuizIndex' });
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
