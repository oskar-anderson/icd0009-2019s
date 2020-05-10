<template>
    <div>
        <h1>Index Meals</h1>

        <p>
            <router-link to="Meal/Create">Create new</router-link>
        </p>

        <ul class="navbar-nav flex-grow-1">
            <li class="nav-item">
                <router-link :to="{ name: 'CategoryIndex' }">Category Index</router-link>
            </li>
        </ul>

        <table class="table">
            <thead>
                <tr>
                    <th>
                        Category
                    </th>
                    <th>
                        Name
                    </th>
                    <th>
                        Picture
                    </th>
                    <th>
                        Description
                    </th>

                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="meal in getMealsWithCategory" :key="meal.id">
                    <td>
                        {{meal.categoryName}}
                    </td>
                    <td>
                        {{meal.name}}
                    </td>
                    <td>
                        {{meal.picture}}
                    </td>
                    <td>
                        {{meal.description}}
                    </td>

                    <td>
                        <router-link :to="{ name: 'MealEdit', params: { id: meal.id }}">Edit</router-link> |
                        <router-link :to="{ name: 'MealDetails', params: { id: meal.id }}">Details</router-link> |
                        <router-link :to="{ name: 'MealDelete', params: { id: meal.id }}">Delete</router-link>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IMeal } from "../../domain/IMeal";
import store from "../../store";
import { ICategory } from '../../domain/ICategory';

@Component
export default class MealIndex extends Vue {
    /*
    get getMeals(): IMeal[] {
        return store.state.meals;
    }

    get getCategorys(): ICategory[] {
        return store.state.categorys;
    }
    */

    get getMealsWithCategory(): IMeal[] {
        store.state.meals.forEach(meal => {
            store.state.categorys.forEach(category => {
                if (meal.categoryId == category.id) {
                    meal.categoryName = category.name;
                }
            })
        });
        return store.state.meals;
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
        store.dispatch("getMeals");
        store.dispatch("getCategorys");
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
