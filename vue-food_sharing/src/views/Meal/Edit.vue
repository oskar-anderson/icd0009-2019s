<template>
    <div>
        <h1>Edit</h1>

        <h4>Meal</h4>
        <hr>
        <div class="row">
            <div class="col-md-4">
                <form submit.trigger="onSubmit($event)">

                    <div class="form-group">
                        <label class="control-label" for="CategoryId">CategoryId</label>
                        <select class="form-control" data-val="true" data-val-required="The CategoryId field is required." id="CategoryId" name="CategoryId"><option selected="selected" value="541af15a-8a48-4534-9b01-08d7f05f6234">Salatid</option>
                            <!--
                            <option value="6f352a82-830e-4e17-9b02-08d7f05f6234">Pastad</option>
                            <option value="bc62f10c-3288-4a16-9b03-08d7f05f6234">Pitsad</option>
                            <option value="810d0919-9c81-4cbd-9b04-08d7f05f6234">Sn&#xE4;kid</option>
                            <option value="66082e0d-2e77-4dfb-9b05-08d7f05f6234">Magustoidud</option>
                            <option value="e7f08d47-ba26-43a2-9b06-08d7f05f6234">Alkohoolsed joogid</option>
                            <option value="31d95990-c5e8-4882-9b07-08d7f05f6234">Alkoholivabad joogid</option>
                            -->
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Name">Name</label>
                        <input class="form-control" type="text" data-val="true" data-val-required="The Name field is required." id="Name" name="Name" value="Caesari Salat" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="Name" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Picture">Picture</label>
                        <input class="form-control" type="text" id="Picture" name="Picture" value="" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="Picture" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Description">Description</label>
                        <input class="form-control" type="text" id="Description" name="Description" value="" />
                        <span class="text-danger field-validation-valid" data-valmsg-for="Description" data-valmsg-replace="true"></span>
                    </div>
                    <!--
                    <div class="form-group">
                        <input type="submit" value="Submit" class="btn btn-primary" />
                    </div>
                    -->
                    <button @click="updateOnClick(getMeal)" type="button" class="btn btn-primary">Submit</button>
                </form>
            </div>
        </div>

        <div>
            <router-link :to="{ name: 'MealIndex' }">Back to list</router-link>
        </div>

    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { ICategory } from "../../domain/ICategory";
import store from "../../store";
import router from '../../router';
import { IMeal } from '../../domain/IMeal';


const populateDropdown = () => {
    let select = document.getElementById("CategoryId")
    let options: ICategory[] = store.state.categorys
    options.forEach(opt => {
    
        let el = document.createElement("option");
        el.value = opt.id;
        el.text = opt.name;
    
        if(select === null) {
            return;
        }
        else{
            select.appendChild(el);
        } 
    });
}

@Component
export default class MealEdit extends Vue {
    get getCategorys(): ICategory[] {
        return store.state.categorys;
    }

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

    updateOnClick(meal: IMeal): void {
        console.log("Clicked on updateOnClick button");
        store.dispatch('editMeal', meal);
        router.push({ name: 'MealIndex' });
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
        populateDropdown()
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
