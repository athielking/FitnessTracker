import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

import { ISaveLog, ILog, ISingleLog, SingleLog, SaveLog } from '../../shared/models/log';
import { NotificationService } from 'src/app/core/services/notification.service';
import { LogService } from '../log.service';

@Component({
    templateUrl: './log-edit.component.html',
    styleUrls: ['../log.css']
  })
  export class LogEditComponent implements OnInit, OnDestroy {

    log:ISingleLog = undefined;
    errorMessage:string = ""

    private _onDestroy = new Subject();

    constructor(private route: ActivatedRoute, private logService:LogService, private notifyService : NotificationService) { }

    ngOnInit(): void {
        let id:string = "";
        this.route.paramMap.subscribe( (params: ParamMap) => {
            id = params.get('id');
        })

        if(id){
            this.getLogById(id);
        }
    }

    
    ngOnDestroy(): void {
        //this._onDestroy.next();
        this._onDestroy.complete();
    }

    editLog($event){
        let log:ISaveLog = new SaveLog($event);
        this.logService.updateLog(log).subscribe( (data: ILog) =>{
            this.notifyService.showSuccess("Log was Updated");
        },
            (error: any) => {
                console.log(error)
                this.notifyService.showError("Failed to update log");
            }

        )
    }

    private getLogById(id){
        this.logService.getLogById(id).pipe(takeUntil(this._onDestroy)).subscribe({
            next:(log:ILog) => {
                this.log = this.parseLog(log)
            },
            error:err => this.errorMessage = err
            })
    }

    private parseLog(log:ILog):ISingleLog{
        return new SingleLog(log, log.logExercises[0]);
    }   
}