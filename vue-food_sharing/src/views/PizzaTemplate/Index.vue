<template>
    <div>
        <h1>Index PizzaTemplates</h1>

        <p>
            <router-link to="PizzaTemplate/Create">Create new</router-link>
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
                        Modifications
                    </th>
                    <th>
                        Extras
                    </th>
                    <th>
                        Description
                    </th>
                    <th>
                        VarietyState
                    </th>

                    <th>
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="pizzaTemplate in getPizzaTemplates" :key="pizzaTemplate.id">
                    <td>
                        {{pizzaTemplate.category.name}}
                    </td>
                    <td>
                        {{pizzaTemplate.name}}
                    </td>
                    <td>
                        {{pizzaTemplate.picture}}
                    </td>
                    <td>
                        {{pizzaTemplate.modifications}}
                    </td>
                    <td>
                        {{pizzaTemplate.extras}}
                    </td>
                    <td>
                        {{pizzaTemplate.description}}
                    </td>
                    <td>
                        {{pizzaTemplate.varietyState}}
                    </td>

                    <td>
                        <router-link :to="{ name: 'PizzaTemplateEdit', params: { id: pizzaTemplate.id }}">Edit</router-link> |
                        <router-link :to="{ name: 'PizzaTemplateDetails', params: { id: pizzaTemplate.id }}">Details</router-link> |
                        <router-link :to="{ name: 'PizzaTemplateDelete', params: { id: pizzaTemplate.id }}">Delete</router-link>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IPizzaTemplate } from "../../domain/IPizzaTemplate";
import store from "../../store";
import { ICategory } from '../../domain/ICategory';

@Component
export default class PizzaTemplateIndex extends Vue {

    get getPizzaTemplates(): IPizzaTemplate[] {
        return store.state.pizzaTemplates;
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
        store.dispatch("getPizzaTemplates");
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
