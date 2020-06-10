<template>
    <div v-if="getJWT != null">
        <h1>Index Choices</h1>

        <p>
            <router-link to="Choice/Create">Create new</router-link>
        </p>

        <table class="table">
            <thead>
                <tr>
                    <th>
                        QuestionId
                    </th>
                    <th>
                        Name
                    </th>
                    <th>
                        Gives Points
                    </th>

                    <th>
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="choice in getChoices" :key="choice.id">
                    <td>
                        {{choice.questionId}}
                    </td>
                    <td>
                        {{choice.name}}
                    </td>
                    <td>
                        {{choice.givesPoints}}
                    </td>
                    <td>
                        <router-link :to="{ name: 'ChoiceEdit', params: { id: choice.id }}">Edit</router-link>
                        <button @click="onDelete(choice)" type="button" class="btn btn-danger">Delete</button>
                    </td>

                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IChoice } from "../../domain/IChoice";
import store from "../../store";

@Component
export default class ChoiceIndex extends Vue {
    get getChoices(): IChoice[] {
        return store.state.choices;
    };

    get getJWT(){
        return store.state.jwt;
    }

    onDelete(choice: IChoice): void {
        console.log("clicked on delete button!")
        store.dispatch('deleteChoice', choice.id)
        router.push({ name: 'ChoiceIndex' });
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
