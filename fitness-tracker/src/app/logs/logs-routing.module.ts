import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogListComponent } from './log-list/log-list.component';
import { LogDetailComponent } from './log-detail/log-detail.component';

const logRoutes: Routes = [
    {path: 'logs', component: LogListComponent},
    {path: 'logs/:id', component: LogDetailComponent}
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

