import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { UsersRoutingModule } from './users-routing.module';

import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserAccountRegisterComponent } from './user-account/user-account-register.component';
import { UserAccountLoginComponent } from './user-account/user-account-login.component';


@NgModule({
    declarations: [
        UserListComponent,
        UserEditComponent,
        UserAccountRegisterComponent,
        UserAccountLoginComponent
    ],
    imports:[
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        UsersRoutingModule
    ]
})

export class UsersModule {} 