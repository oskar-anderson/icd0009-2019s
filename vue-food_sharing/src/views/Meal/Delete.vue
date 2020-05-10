<template>
    <div>
        <h1>Delete</h1>

        <h3>Are you sure you want to delete this?</h3>
        <div>
            <h4>Restaurant</h4>
            <hr />
            <dl class="row">

                <dt class="col-sm-2">
                    Name
                </dt>
                <dd class="col-sm-10">
                    {{getRestaurant.name}}
                </dd>
                <dt class="col-sm-2">
                    Location
                </dt>
                <dd class="col-sm-10">
                    {{getRestaurant.location}}
                </dd>
                <dt class="col-sm-2">
                    Telephone
                </dt>
                <dd class="col-sm-10">
                    {{getRestaurant.telephone}}
                </dd>
                <dt class="col-sm-2">
                    OpenTime
                </dt>
                <dd class="col-sm-10">
                    {{getRestaurant.openTime}}
                </dd>
                <dt class="col-sm-2">
                    OpenNotification
                </dt>
                <dd class="col-sm-10">
                    {{getRestaurant.openNotification}}
                </dd>
            </dl>

            <button @click="onSubmit(getRestaurant)" type="button" class="btn btn-danger">Delete</button>
            <router-link :to="{ name: 'RestaurantIndex' }">Back to list</router-link>

        </div>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IRestaurant } from "../../domain/IRestaurant";
import store from "../../store";

@Component
export default class RestaurantDelete extends Vue {
    get getRestaurant(): IRestaurant {
        const restaurant = store.state.restaurant;
        const restaurantDummy = {
            id: "",
            name: "",
            location: "",
            telephone: "",
            openTime: "",
            openNotification: ""
        };
        return restaurant != null ? restaurant : restaurantDummy;
    }

    onSubmit(event: IRestaurant): void {
        console.log("clicked on delete button!")
        store.dispatch('deleteRestaurant', event.id)
        router.push({ name: 'RestaurantIndex' });
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
        store.dispatch("getRestaurant", this.$route.params.id)
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
