<template>
    <div>
        <h1>Delete</h1>

        <h3>Are you sure you want to delete this?</h3>
        <div>
            <h4>PizzaTemplate</h4>
            <hr />
            <dl class="row">

                <dt class = "col-sm-2">
                    Category
                </dt>
                <dd class = "col-sm-10">
                    {{getPizzaTemplate.category.name}}
                </dd>
                <dt class = "col-sm-2">
                    Name
                </dt>
                <dd class = "col-sm-10">
                    {{getPizzaTemplate.name}}
                </dd>
                <dt class = "col-sm-2">
                    Picture
                </dt>
                <dd class = "col-sm-10">
                    {{getPizzaTemplate.picture}}
                </dd>
                <dt class = "col-sm-2">
                    Modifications
                </dt>
                <dd class = "col-sm-10">
                    {{getPizzaTemplate.modifications}}
                </dd>
                <dt class = "col-sm-2">
                    Extras
                </dt>
                <dd class = "col-sm-10">
                    {{getPizzaTemplate.extras}}
                </dd>
                <dt class = "col-sm-2">
                    Description
                </dt>
                <dd class = "col-sm-10">
                    {{getPizzaTemplate.description}}
                </dd>
                <dt class = "col-sm-2">
                    VarietyState
                </dt>
                <dd class = "col-sm-10">
                    {{getPizzaTemplate.varietyState}}
                </dd>
            </dl>

            <button @click="onSubmit(getPizzaTemplaate)" type="button" class="btn btn-danger">Delete</button>
            <router-link :to="{ name: 'PizzaTemplateIndex' }">Back to list</router-link>

        </div>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IPizzaTemplate } from "../../domain/IPizzaTemplate";
import store from "../../store";

@Component
export default class PizzaTemplateDelete extends Vue {
    get getPizzaTemplate(): IPizzaTemplate {
        const pizzaTemplate = store.state.pizzaTemplate;
        const pizzaTemplateDummy = {
            id: "",
            categoryId: "",
            categoryName: "",
            name: "",
            picture: "",
            modifications: 0,
            extras: 0,
            description: "",
            varietyState: 0,
        };
        return pizzaTemplate != null ? pizzaTemplate : pizzaTemplateDummy;
    }

    onSubmit(event: IPizzaTemplate): void {
        console.log("clicked on delete button!")
        store.dispatch('deletePizzaTemplate', event.id)
        router.push({ name: 'PizzaTemplateIndex' });
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
        store.dispatch("getPizzaTemplate", this.$route.params.id)
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
