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

                <h4>Manage Email</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <form id="email-form" method="post">
                                <div class="form-group">
                                    <label for="Email">Email</label>
                                    <div class="input-group">
                                        <input v-model="userEmail" class="form-control" disabled="" type="text" data-val="true" data-val-required="The Email field is required." id="Email" name="Email">
                                        <div class="input-group-append">
                                            <span class="input-group-text text-success font-weight-bold">✓</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="Input_NewEmail">New email</label>
                                    <input v-model="userEmail" class="form-control" type="email" data-val="true" data-val-email="The New email field is not a valid e-mail address." data-val-required="The New email field is required." id="Input_NewEmail" name="Input.NewEmail">
                                </div>
                                <button id="change-email-button" type="submit" class="btn btn-primary" formaction="/Identity/Account/Manage/Email?handler=ChangeEmail">Change email</button>
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