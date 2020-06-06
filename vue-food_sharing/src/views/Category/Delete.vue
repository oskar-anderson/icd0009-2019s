<template>
    <div>
        <h1>Delete</h1>

        <h3>Are you sure you want to delete this?</h3>
        <div>
            <h4>Category</h4>
            <hr />
            <dl class="row">

                <dt class="col-sm-2">
                    Name
                </dt>
                <dd class="col-sm-10">
                    {{getCategory.name}}
                </dd>
            </dl>

            <button @click="onSubmit(getCategory)" type="button" class="btn btn-danger">Delete</button>
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
export default class CategoryDelete extends Vue {
    get getCategory(): ICategory {
        const category = store.state.category;
        const categoryDummy = {
            id: "",
            name: "",
            location: "",
            telephone: "",
            openTime: "",
            openNotification: ""
        };
        return category != null ? category : categoryDummy;
    }

    onSubmit(event: ICategory): void {
        console.log("clicked on delete button!")
        store.dispatch('deleteCategory', event.id)
        router.push({ name: 'CategoryIndex' });
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
