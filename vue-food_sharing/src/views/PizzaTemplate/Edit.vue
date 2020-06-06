<template>
    <div>
        <h1>Edit</h1>

        <h4>PizzaTemplate</h4>
        <hr>
        <div class="row">
            <div class="col-md-4">
                <form submit.trigger="onSubmit($event)">

                    <div class="form-group">
                        <label class="control-label" for="CategoryId">CategoryId</label>
                        <select class="form-control" data-val="true" data-val-required="The CategoryId field is required." 
                        id="CategoryId" name="CategoryId" v-model="getPizzaTemplate.categoryId">
                            <option
                                v-for="category in getCategorys"
                                :key="category.id"
                                v-bind:value="category.id">
                                {{ category.name }}
                            </option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Name">Name</label>
                        <input class="form-control" type="text" data-val="true" data-val-required="The Name field is required." id="Name" name="Name" v-model="getPizzaTemplate.name" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="Name" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Picture">Picture</label>
                        <input class="form-control" type="text" id="Picture" name="Picture" v-model="getPizzaTemplate.picture" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="Picture" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Modifications">Modifications</label>
                        <input class="form-control" type="text" id="Modifications" name="Modifications" v-model="getPizzaTemplate.modifications" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="Modifications" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Extras">Extras</label>
                        <input class="form-control" type="text" id="Extras" name="Extras" v-model="getPizzaTemplate.extras" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="Extras" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Description">Description</label>
                        <input class="form-control" type="text" id="Description" name="Description" v-model="getPizzaTemplate.description" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="Description" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="VarietyState">VarietyState</label>
                        <input class="form-control" type="text" id="VarietyState" name="VarietyState" v-model="getPizzaTemplate.varietyState" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="VarietyState" data-valmsg-replace="true"></span>
                    </div>

                    <!--
                    <div class="form-group">
                        <input type="submit" value="Submit" class="btn btn-primary" />
                    </div>
                    -->
                    <button @click="updateOnClick(getPizzaTemplate)" type="button" class="btn btn-primary">Submit</button>
                </form>
            </div>
        </div>

        <div>
            <router-link :to="{ name: 'PizzaTemplateIndex' }">Back to list</router-link>
        </div>

    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { ICategory } from "../../domain/ICategory";
import store from "../../store";
import router from '../../router';
import { IPizzaTemplate } from '../../domain/IPizzaTemplate';

@Component
export default class PizzaTemplateEdit extends Vue {
    get getCategorys(): ICategory[] {
        return store.state.categorys;
    }

    get getPizzaTemplate(): IPizzaTemplate {
        const PizzaTemplate = store.state.pizzaTemplate;
        const PizzaTemplateNotFound = {
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
        return PizzaTemplate != null ? PizzaTemplate : PizzaTemplateNotFound;
    }

    updateOnClick(PizzaTemplate: IPizzaTemplate): void {
        console.log("Clicked on updateOnClick button");
        store.dispatch('editPizzaTemplate', PizzaTemplate);
        router.push({ name: 'PizzaTemplateIndex' });
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
