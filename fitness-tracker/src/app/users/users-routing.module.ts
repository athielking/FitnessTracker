import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { AuthGuard } from '../auth/auth.guard';

const routes: Routes = [
    {path: 'users', component: UserListComponent, canActivate: [AuthGuard]},
    {path: 'users/:id', component: UserEditComponent, canActivate: [AuthGuard]}
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