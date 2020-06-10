<template>
    <div v-if="getJWT != null">
        <h1>Index Results</h1>

        <p>
            <router-link to="Result/Create">Create new</router-link>
        </p>

        <table class="table">
            <thead>
                <tr>
                    <th>
                        Quiz
                    </th>
                    <th>
                        PersonName
                    </th>
                    <th>
                        Score
                    </th>
                    <th>
                        Answers
                    </th>

                    <th>
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="result in getResults" :key="result.id">
                    <td>
                        {{result.quizId}}
                    </td>
                    <td>
                        {{result.personName}}
                    </td>
                    <td>
                        {{result.score}}
                    </td>
                    <td>
                        {{result.questionToPickedAnswer}}
                    </td>
                    <td>
                        <router-link :to="{ name: 'ResultEdit', params: { id: result.id }}">Edit</router-link>
                        <button @click="onDelete(result)" type="button" class="btn btn-danger">Delete</button>
                    </td>

                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IResult } from "../../domain/IResult";
import store from "../../store";

@Component
export default class ResultIndex extends Vue {
    get getResults(): IResult[] {
        return store.state.results;
    }

    get getJWT(){
        return store.state.jwt;
    }

    onDelete(result: IResult): void {
        console.log("clicked on delete button!")
        store.dispatch('deleteResult', result.id)
        router.push({ name: 'ResultIndex' });
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
