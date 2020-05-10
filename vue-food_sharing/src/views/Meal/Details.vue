<template>
    <div>
        <h1>Details</h1>

        <div>
            <h4>Meal</h4>
            <hr />
            <dl class="row">
                <dt class = "col-sm-2">
                    Category
                </dt>
                <dd class = "col-sm-10">
                    {{getMeal.categoryName}}
                </dd>
                <dt class = "col-sm-2">
                    Name
                </dt>
                <dd class = "col-sm-10">
                    {{getMeal.name}}
                </dd>
                <dt class = "col-sm-2">
                    Picture
                </dt>
                <dd class = "col-sm-10">
                    {{getMeal.picture}}
                </dd>
                <dt class = "col-sm-2">
                    Description
                </dt>
                <dd class = "col-sm-10">
                    {{getMeal.description}}
                </dd>
            </dl>
        </div>
        <div>
            <router-link :to="{ name: 'MealEdit', params: { id: $route.params.id }}">Edit</router-link> |
            <router-link :to="{ name: 'MealIndex' }">Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IMeal } from "../../domain/IMeal";
import store from "../../store";

@Component
export default class MealDetails extends Vue {
    get getMeal(): IMeal {
        const meal = store.state.meal;
        const mealNotFound = {
            id: "",
            categoryId: "",
            categoryName: "",
            name: "",
            picture: "",
            description: ""
        };
        return meal != null ? meal : mealNotFound;
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
        store.dispatch("getMeal", this.$route.params.id)
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
