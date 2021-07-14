import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';
import { FormsModule, ReactiveFormsModule }    from '@angular/forms';
import {ToastrModule} from 'ngx-toastr'

import { LogsRoutingModule } from './logs-routing.module';

import { LogListComponent } from './log-list/log-list.component';
import { LogEditComponent } from './log-edit/log-edit.component';
import { LogFormComponent } from './log-form/log-form.component';
import { LogDetailComponent } from './log-detail/log-detail.component';
import { LogAddSetComponent } from './log-add/log-add-set.component';
import { LogCreateComponent } from './log-create/log-create.component';

@NgModule({
    declarations: [
        LogListComponent,
        LogFormComponent,  
        LogDetailComponent,           
        LogEditComponent,
        LogAddSetComponent,
        LogCreateComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        LogsRoutingModule,
        ToastrModule
    ]
})
export class LogsModule {} 