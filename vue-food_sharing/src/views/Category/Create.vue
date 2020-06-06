<template>
    <div>
        <h1>Create</h1>
        <h4>Category</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form onload="category()" submit.trigger="onSubmit($event)">

                    <div class="form-group">
                        <label class="control-label" for="Name">Name</label>
                        <input class="form-control" type="text" id="Name" maxlength="64" v-model="getCategory.name"/>
                    </div>
                    <!--
                    <div class="form-group">
                        <input type="submit" value="Create" class="btn btn-primary" />
                    </div>
                    -->
                    <button @click="createOnClick(getCategory)" type="button" class="btn btn-primary">Create</button>
                </form>
            </div>
        </div>

        <div>
            <router-link :to="{ name: 'CategoryIndex' }">Back to list</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { ICategory } from "../../domain/ICategory";
import store from "../../store";

@Component
export default class CategoryCreate extends Vue {
    get getCategory(): ICategory {
        const category = {
            id: "",
            name: "",
        };
        return category;
    };

    createOnClick(category: ICategory): void {
        console.log("Clicked on createOnClick button");
        store.dispatch('createCategory', category);
        router.push({ name: 'CategoryIndex' });
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
