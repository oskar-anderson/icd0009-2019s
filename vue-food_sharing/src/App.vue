<template>
    <div>
        <header>
            <nav
                class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3"
            >
                <div class="container">
                    <router-link to="/" class="navbar-brand">Home</router-link>
                    <button
                        class="navbar-toggler"
                        type="button"
                        data-toggle="collapse"
                        data-target=".navbar-collapse"
                        aria-controls="navbarSupportedContent"
                        aria-expanded="false"
                        aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                        <Identity />
                        <ul class="navbar-nav flex-grow-1">
                            <li class="nav-item">
                                <router-link to="/" class="nav-link text-dark">Home</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link to="/PizzaTemplate" class="nav-link text-dark">PizzaTemplate</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link to="/Restaurant" class="nav-link text-dark">Restaurant</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link to="/Category" class="nav-link text-dark">Category</router-link>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
        <div class="container">
            <main role="main" class="pb-3">
                <router-view />
            </main>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import Identity from "./components/Identity.vue";

@Component({
    components: {
        Identity
    }
})
export default class App extends Vue {}
</script>
