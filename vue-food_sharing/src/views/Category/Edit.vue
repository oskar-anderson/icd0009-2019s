<template>
    <div>
        <h1>Edit</h1>

        <h4>Category</h4>
        <hr>
        <div class="row">
            <div class="col-md-4">
                <form submit.trigger="onSubmit($event)">

                    <div class="form-group">
                        <label class="control-label" for="Name">Name</label>
                        <input class="form-control" type="text" id="Name" maxlength="64" v-model="getCategory.name" />
                    </div>

                    <!--
                    <div class="form-group">
                        <input type="submit" value="Submit" class="btn btn-primary" />
                    </div>
                    -->
                    <button @click="updateOnClick(getCategory)" type="button" class="btn btn-primary">Submit</button>
                </form>
            </div>
        </div>

        <div>
            <router-link :to="{ name: 'CategoryIndex' }">Back to list</router-link>
        </div>

    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { ICategory } from "../../domain/ICategory";
import store from "../../store";
import router from '../../router';

@Component
export default class CategoryEdit extends Vue {
    get getCategory(): ICategory {
        const category = store.state.category;
        const categoryNotFound = {
            id: "",
            name: "",
            location: "",
            telephone: "",
            openTime: "",
            openNotification: ""
        };
        return category != null ? category : categoryNotFound;
    }

    updateOnClick(category: ICategory): void {
        console.log("Clicked on updateOnClick button");
        store.dispatch('editCategory', category);
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
        store.dispatch("getCategory", this.$route.params.id)
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
