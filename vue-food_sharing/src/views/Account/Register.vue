<template>
    <div>
        <h1>Register</h1>
        <div class="row">
            <div class="col-md-4">
                <h4>Create a new account.</h4>
                <hr />

                <div class="form-group">
                    <label for="Input_FirstName">FirstName</label>
                    <input v-model="registerInfo.firstName" class="form-control" type="text" data-val="true" data-val-maxlength="The field FirstName must be a string or array type with a maximum length of &#x27;128&#x27;." data-val-maxlength-max="128" data-val-minlength="The field FirstName must be a string or array type with a minimum length of &#x27;1&#x27;." data-val-minlength-min="1" data-val-required="The FirstName field is required." id="Input_FirstName" maxlength="128" name="Input.FirstName" />
                </div>
                <div class="form-group">
                    <label for="Input_LastName">LastName</label>
                    <input v-model="registerInfo.lastName" class="form-control" type="text" data-val="true" data-val-maxlength="The field LastName must be a string or array type with a maximum length of &#x27;128&#x27;." data-val-maxlength-max="128" data-val-minlength="The field LastName must be a string or array type with a minimum length of &#x27;1&#x27;." data-val-minlength-min="1" data-val-required="The LastName field is required." id="Input_LastName" maxlength="128" name="Input.LastName" />
                </div>
                <div class="form-group">
                    <label for="Input_Phone">Phone</label>
                    <input v-model="registerInfo.phone" class="form-control" type="text" data-val="true" data-val-maxlength="The field Phone must be a string or array type with a maximum length of &#x27;16&#x27;." data-val-maxlength-max="16" data-val-minlength="The field Phone must be a string or array type with a minimum length of &#x27;1&#x27;." data-val-minlength-min="1" data-val-required="The Phone field is required." id="Input_Phone" maxlength="16" name="Input.Phone" />
                </div>
                <div class="form-group">
                    <label for="Input_Email">Email</label>
                    <input v-model="registerInfo.email" class="form-control" type="email" data-val="true" data-val-email="The Email field is not a valid e-mail address." data-val-maxlength="The field Email must be a string or array type with a maximum length of &#x27;128&#x27;." data-val-maxlength-max="128" data-val-minlength="The field Email must be a string or array type with a minimum length of &#x27;1&#x27;." data-val-minlength-min="1" data-val-required="The Email field is required." id="Input_Email" maxlength="128" name="Input.Email"/>
                </div>
                <div class="form-group">
                    <label for="Input_Password">Password</label>
                    <input v-model="registerInfo.password" class="form-control" type="password" data-val="true" data-val-length="The Password must be at least 6 and at max 100 characters long." data-val-length-max="100" data-val-length-min="6" data-val-required="The Password field is required." id="Input_Password" maxlength="100" name="Input.Password" />
                </div>
                <div class="form-group">
                    <label for="Input_ConfirmPassword">Confirm password</label>
                    <input v-model="registerInfo.confirmPassword" class="form-control" type="password" data-val="true" data-val-equalto="The password and confirmation password do not match." data-val-equalto-other="*.Password" data-val-required="The Confirm password field is required." id="Input_ConfirmPassword" name="Input.ConfirmPassword" />
                </div>
                
                <div class="form-group">
                    <button @click="registerOnClick($event)" class="btn btn-primary">Register</button>
                </div>
            </div>
        </div>
    </div>
</template>


<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { ILoginDTO } from "@/types/ILoginDTO";
import { IRegisterDTO } from '@/types/IRegisterDTO';
import store from "../../store";
import router from "../../router";

@Component
export default class RegisterIndex extends Vue {
    private registerInfo: IRegisterDTO = {
        firstName: "",
        lastName: "",
        phone: "",
        email: "",
        password: "",
        confirmPassword: "",
        errorMessage: ""
    };

    getErrorMsg(): string {
        return this.registerInfo.errorMessage;
    }

    private registerWasOk: boolean | null = null;

    registerOnClick(): void {
        if (this.registerInfo.email.length > 0 &&
            this.registerInfo.password.length > 0) {
            console.log(this.registerInfo.email, this.registerInfo.password);
            store.dispatch("registerUser", this.registerInfo);
            router.push("/");
        };
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