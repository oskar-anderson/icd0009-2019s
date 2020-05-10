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

                    <h4>Change password</h4>


                    <div class="row">
                        <div class="col-md-6">
                            <form id="change-password-form" method="post">
                                <div class="form-group">
                                    <label for="Input_OldPassword">Current password</label>
                                    <input class="form-control" type="password" data-val="true" data-val-required="The Current password field is required." id="Input_OldPassword" name="Input.OldPassword" />
                                </div>
                                <div class="form-group">
                                    <label for="Input_NewPassword">New password</label>
                                    <input class="form-control" type="password" data-val="true" data-val-length="The New password must be at least 6 and at max 100 characters long." data-val-length-max="100" data-val-length-min="6" data-val-required="The New password field is required." id="Input_NewPassword" maxlength="100" name="Input.NewPassword" />
                                </div>
                                <div class="form-group">
                                    <label for="Input_ConfirmPassword">Confirm new password</label>
                                    <input class="form-control" type="password" data-val="true" data-val-equalto="The new password and confirmation password do not match." data-val-equalto-other="*.NewPassword" data-val-required="The Confirm new password field is required." id="Input_ConfirmPassword" name="Input.ConfirmPassword" />
                                </div>
                                <button type="submit" class="btn btn-primary">Update password</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>