<template>
    <div>
        <h1>Manage your account</h1>
        <div>
            <h4>Change your account settings</h4>
            <hr />
            <div class="row">
                <div class="col-md-3">
                    <ul class="nav nav-pills flex-column">
                        <li class="nav-item">
                            <router-link :to="{ name: 'AccountManageProfile' }" class="nav-link">Profile</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link :to="{ name: 'AccountManageEmail' }" class="nav-link">Email</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link :to="{ name: 'AccountManagePassword' }" class="nav-link">Password</router-link>
                        </li>
                    </ul>
                </div>
                <div class="col-md-9">

                <h4>Profile</h4>

                    <div class="row">
                        <div class="col-md-6">
                            <form id="profile-form" method="post">
                                <div class="form-group">
                                    <label for="Username">Username</label>
                                    <input v-model="userEmail" class="form-control" disabled type="text" data-val="true" data-val-required="The Username field is required." id="Username" name="Username" />
                                </div>
                                <div class="form-group">
                                    <label for="Input_PhoneNumber">Phone number</label>
                                    <input value="" class="form-control" type="tel" data-val="true" data-val-phone="The Phone number field is not a valid phone number." data-val-required="The Phone number field is required." id="Input_PhoneNumber" name="Input.PhoneNumber" />
                                </div>
                                <button id="update-profile-button" type="submit" class="btn btn-primary">Save</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "../../store";
import router from '../../router';
import JwtDecode from 'jwt-decode';

@Component
export default class AccountManageProfile extends Vue {
    get userEmail(): string {
        if (store.state.jwt) {
            const decoded = JwtDecode(store.state.jwt) as Record<string, string>;
            return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        }
        return 'null';
    }
}
</script>