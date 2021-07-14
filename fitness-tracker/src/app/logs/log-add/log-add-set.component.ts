import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { LogService } from '../log.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { ILog, ILogExercise, ISaveLog, ISingleLog, createSaveLog, } from 'src/app/shared/models/log';


@Component({
    selector: 'log-add-set',
    templateUrl: './log-add-set.component.html',
    styleUrls: ['../log.css']
  })

export class LogAddSetComponent implements OnInit, OnDestroy{

    log:ISingleLog
    errorMessage:string = ""

    private _onDestroy$ = new Subject();
    
    constructor(private logService:LogService, private notifyService : NotificationService,private route: Router, private activatedRoute: ActivatedRoute) { }

    ngOnInit(): void {
        let id:string = undefined;
        let date:string = "";
        this.activatedRoute.paramMap.subscribe( (params: ParamMap) =>{
            id = params.get('id');
            date = params.get('date');
            id = id ? id : this.log.logId.toString();
            date = date ? date : this.log.created;
        })
        
        this.createLogBySet(id, date);
      }
       
    ngOnDestroy(): void {
        if(this._onDestroy$){
            this._onDestroy$.next(null);
            this._onDestroy$.unsubscribe();
        }
    }

    saveLog($event){
        let log:ISaveLog = createSaveLog($event);
        this.logService.addLog(log).subscribe( (data: ILog) =>{
            this.notifyService.showSuccess("Log was Created");
        },
            (error: any) => {
                console.log(error)
                this.notifyService.showError("Failed to create log");
            }

        )
    }

    private createLogBySet(id, date){
        this.logService.getLogBySet(id, date).pipe(takeUntil(this._onDestroy$)).subscribe({
            next:(logs:ILog[]) => {
                let log = logs.pop()
                this.log = this.createSingleLog(log, log.logExercises[0]);
            },
                error:err => this.errorMessage = err
            })
    }

    private createSingleLog(log:ILog, logExercises:ILogExercise):ISingleLog{
        const tmplog:ISingleLog = {
            logId: undefined,
            user: log.user,
            set: log.set + 1,
            setId: log.setId,
            comments: log.comments,
            created: log.created,
            exerciseId: logExercises.exerciseId,
            exerciseName: logExercises.exerciseName,
            reps: 0,
            weight: 0,
            targetRep: 0,
        }

        return tmplog;
    }
}