import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserCreateComponent } from './user-create/user-create.component';

const routes: Routes = [
    {path: 'users', component: UserListComponent},
    {path: 'users/:id', component: UserEditComponent},
    {path: 'newuser', component: UserCreateComponent}
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