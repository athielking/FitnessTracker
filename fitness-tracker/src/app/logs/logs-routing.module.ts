import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LogListComponent } from './log-list/log-list.component';
import { LogEditComponent } from './log-edit/log-edit.component';
import { LogAddSetComponent } from './log-add/log-add-set.component';

const logRoutes: Routes = [
    {path: 'logs', component: LogListComponent},
    {path: 'logs-edit/:id/:date', component: LogEditComponent},    
    {path: 'logs-edit/:id', component: LogEditComponent},
    {path: 'logs-user/:name', component: LogListComponent},
    {path: 'log-add-set/:id/:date', component: LogAddSetComponent}
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

