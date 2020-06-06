<template>
    <div>
        <h1>Details</h1>

        <div>
            <h4>Category</h4>
            <hr />
            <dl class="row">
                <dt class="col-sm-2">
                    ID
                </dt>
                <dd class="col-sm-10">
                    {{$route.params.id}}
                </dd>
                <dt class="col-sm-2">
                    Name
                </dt>
                <dd class="col-sm-10">
                    {{getCategory.name}}
                </dd>
            </dl>
        </div>
        <div>
            <router-link :to="{ name: 'CategoryEdit', params: { id: $route.params.id }}">Edit</router-link> |
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
export default class CategoryDetails extends Vue {
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
