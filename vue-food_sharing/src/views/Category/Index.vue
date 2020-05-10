<template>
    <div>
        <h1>Index Categorys</h1>

        <p>
            <router-link to="Category/Create">Create new</router-link>
        </p>

        <table class="table">
            <thead>
                <tr>
                    <th>
                        Name
                    </th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="category in getCategorys" :key="category.id">
                    <td>
                        {{category.name}}
                    </td>

                    <td>
                        <router-link :to="{ name: 'CategoryEdit', params: { id: category.id }}">Edit</router-link> |
                        <router-link :to="{ name: 'CategoryDetails', params: { id: category.id }}">Details</router-link> |
                        <router-link :to="{ name: 'CategoryDelete', params: { id: category.id }}">Delete</router-link>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { ICategory } from "../../domain/ICategory";
import store from "../../store";

@Component
export default class CategoryIndex extends Vue {
    get getCategorys(): ICategory[] {
        return store.state.categorys;
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
