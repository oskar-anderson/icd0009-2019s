<template>
    <div>
        <h1>Edit</h1>

        <h4>Restaurant</h4>
        <hr>
        <div class="row">
            <div class="col-md-4">
                <form submit.trigger="onSubmit($event)">

                    <div class="form-group">
                        <label class="control-label" for="Name">Name</label>
                        <input class="form-control" type="text" id="Name" maxlength="64" v-model="getRestaurant.name" />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Location">Location</label>
                        <input class="form-control" type="text" id="Location" maxlength="64" v-model="getRestaurant.location" />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Telephone">Telephone</label>
                        <input class="form-control" type="text" id="Telephone" maxlength="64" v-model="getRestaurant.telephone" />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="OpenTime">OpenTime</label>
                        <input class="form-control" type="text" id="OpenTime" maxlength="64" v-model="getRestaurant.openTime" />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="OpenNotification">OpenNotification</label>
                        <input class="form-control" type="text" id="OpenNotification" maxlength="64" v-model="getRestaurant.openNotification" />
                    </div>
                    <!--
                    <div class="form-group">
                        <input type="submit" value="Submit" class="btn btn-primary" />
                    </div>
                    -->
                    <button @click="updateOnClick(getRestaurant)" type="button" class="btn btn-primary">Submit</button>
                </form>
            </div>
        </div>

        <div>
            <router-link :to="{ name: 'RestaurantIndex' }">Back to list</router-link>
        </div>

    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IRestaurant } from "../../domain/IRestaurant";
import store from "../../store";
import router from '../../router';

@Component
export default class RestaurantEdit extends Vue {
    get getRestaurant(): IRestaurant {
        const restaurant = store.state.restaurant;
        const restaurantNotFound = {
            id: "",
            name: "",
            location: "",
            telephone: "",
            openTime: "",
            openNotification: ""
        };
        return restaurant != null ? restaurant : restaurantNotFound;
    }

    updateOnClick(restaurant: IRestaurant): void {
        console.log("Clicked on updateOnClick button");
        store.dispatch('editRestaurant', restaurant);
        router.push({ name: 'RestaurantIndex' });
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
