import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserAccountRegisterComponent } from './user-account/user-account-register.component';
import { UserAccountLoginComponent } from './user-account/user-account-login.component';

const routes: Routes = [
    {path: 'users', component: UserListComponent},
    {path: 'users/:id', component: UserEditComponent},
    {path: 'register', component: UserAccountRegisterComponent},
    {path: 'login', component: UserAccountLoginComponent},
]

@NgModule({
    imports:[
        RouterModule.forChild(routes)
    ],
    exports:[
        RouterModule
    ]
})
export class UsersRoutingModule{}