import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogListComponent } from './log-list/log-list.component';
import { LogEditComponent } from './log-edit/log-edit.component';

const logRoutes: Routes = [
    {path: 'logs', component: LogListComponent},
    {path: 'logs/:id', component: LogEditComponent}
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

