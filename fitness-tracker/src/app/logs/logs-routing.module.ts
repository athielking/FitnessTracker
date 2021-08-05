import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LogListComponent } from './log-list/log-list.component';
import { LogEditComponent } from './log-edit/log-edit.component';
import { LogAddSetComponent } from './log-add/log-add-set.component';
import { LogCreateComponent } from './log-create/log-create.component';
import { AuthGuard } from '../auth/auth.guard';

const logRoutes: Routes = [
    {path: 'logs', component: LogListComponent, canActivate: [AuthGuard]},
    {path: 'log-create', component: LogCreateComponent, canActivate: [AuthGuard]},
    {path: 'logs-edit/:id/:date', component: LogEditComponent, canActivate: [AuthGuard]},    
    {path: 'logs-edit/:id', component: LogEditComponent, canActivate: [AuthGuard]},
    {path: 'logs-user/:name', component: LogListComponent, canActivate: [AuthGuard]},
    {path: 'log-add-set/:id/:date', component: LogAddSetComponent, canActivate: [AuthGuard]}
]

@NgModule({
    imports:[
        RouterModule.forChild(logRoutes)
    ],
    exports:[
        RouterModule
    ]
})
export class LogsRoutingModule{}

