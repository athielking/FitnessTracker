import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';
import { FormsModule, ReactiveFormsModule }    from '@angular/forms';

import { LogListComponent } from './log-list/log-list.component';
import { LogDetailComponent } from './log-detail/log-detail.component';

import { LogsRoutingModule } from './logs-routing.module';

@NgModule({
    declarations: [
        LogListComponent,
        LogDetailComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        LogsRoutingModule
    ]
})
export class LogsModule {}