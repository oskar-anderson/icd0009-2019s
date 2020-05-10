<template>
    <div>
        <h1>Index Restaurants</h1>

        <p>
            <router-link to="Restaurant/Create">Create new</router-link>
        </p>

        <table class="table">
            <thead>
                <tr>
                    <th>
                        Name
                    </th>
                    <th>
                        Location
                    </th>
                    <th>
                        Telephone
                    </th>
                    <th>
                        OpenTime
                    </th>
                    <th>
                        OpenNotification
                    </th>

                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="restaurant in getRestaurants" :key="restaurant.id">
                    <td>
                        {{restaurant.name}}
                    </td>
                    <td>
                        {{restaurant.location}}
                    </td>
                    <td>
                        {{restaurant.telephone}}
                    </td>
                    <td>
                        {{restaurant.openTime}}
                    </td>
                    <td>
                        {{restaurant.openNotification}}
                    </td>
                    <td>
                        {{restaurant.id}}
                    </td>
                    <td>
                        <router-link :to="{ name: 'RestaurantEdit', params: { id: restaurant.id }}">Edit</router-link> |
                        <router-link :to="{ name: 'RestaurantDetails', params: { id: restaurant.id }}">Details</router-link> |
                        <router-link :to="{ name: 'RestaurantDelete', params: { id: restaurant.id }}">Delete</router-link>
                        <!--
                        <router-link :to="'/restaurant/edit/' + restaurant.id">Edit</router-link> |
                        <router-link :to="'/restaurant/details/' + restaurant.id">Details</router-link> |
                        <router-link :to="'/restaurant/delete/' + restaurant.id">Delete</router-link> |

                        <router-link :to="{ name: 'RestaurantDetails', params: { id: restaurant.id }}">Delete</router-link>
                         -->
                    </td>

                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import router from '../../router';
import { Component, Prop, Vue } from "vue-property-decorator";
import { IRestaurant } from "../../domain/IRestaurant";
import store from "../../store";

@Component
export default class RestaurantIndex extends Vue {
    get getRestaurants(): IRestaurant[] {
        return store.state.restaurants;
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
        store.dispatch("getRestaurants");
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
