<template>
    <ul class="navbar-nav">
        <template v-if="isAuthenticated">
            <li class="nav-item">
                <router-link to="/account/manage/profile" class="nav-link text-dark" title="Manage">Hello {{userEmail}}!</router-link>
            </li>
            <li class="nav-item">
                <a @click="logoutOnClick" class="nav-link text-dark" href>Logout</a>
            </li>
        </template>
        <li v-if="!isAuthenticated" class="nav-item">
            <router-link to="/account/login" class="nav-link text-dark">Login</router-link>
        </li>
        <li v-if="!isAuthenticated" class="nav-item">
            <router-link to="/account/register" class="nav-link text-dark">Register</router-link>
        </li>
    </ul>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "../store";
import router from '../router';
import JwtDecode from 'jwt-decode';

@Component
export default class Identity extends Vue {
    get isAuthenticated(): boolean {
        return store.getters.isAuthenticated;
    }

    get userEmail(): string {
        if (store.state.jwt) {
            const decoded = JwtDecode(store.state.jwt) as Record<string, string>;
            return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        }
        return 'null';
    }

    logoutOnClick(): void {
        store.dispatch("clearJwt");
        router.push("/");
    }
}
</script>
