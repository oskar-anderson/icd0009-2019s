<style>
  @import './assets/css.css';
</style>
<template>
    <div>
        <header>
            <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div class="container">
                    <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                        <Identity />
                        <ul class="navbar-nav flex-grow-1">
                            <li class="nav-item">
                                <router-link to="/" class="nav-link text-dark" style="font-size: 20px">Home</router-link>
                            </li>
                            <li class="nav-item" v-if="getJWT">
                                <router-link to="/Quiz" class="nav-link text-dark">Quiz</router-link>
                            </li>
                            <li class="nav-item" v-if="getJWT">
                                <router-link to="/Question" class="nav-link text-dark">Question</router-link>
                            </li>
                            <li class="nav-item" v-if="getJWT">
                                <router-link to="/Choice" class="nav-link text-dark">Choice</router-link>
                            </li>
                            <li class="nav-item" v-if="getJWT">
                                <router-link to="/Result" class="nav-link text-dark">Result</router-link>
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
import store from "./store";


@Component({
    components: {
        Identity
    }
})

export default class App extends Vue {
    get getJWT(){
        return store.state.jwt;
    }
}

</script>
