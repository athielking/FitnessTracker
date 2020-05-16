import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ISaveLog, ILog, ISingleLog, ILogExercise } from '../log';
import { LogService } from '../log.service';

@Component({
    templateUrl: './log-edit.component.html',
    styleUrls: ['./log-edit.component.css']
  })
  export class LogEditComponent implements OnInit {

    log:ISingleLog = undefined;
    errorMessage:string = ""

    constructor(private route: ActivatedRoute, private logService:LogService) { }

    ngOnInit(): void {
        let id = this.route.snapshot.paramMap.get('id');
        this.logService.getLogByid(id).subscribe({
        next:(log:ILog) => {
            this.log = this.parseLog(log)
        },
        error:err => this.errorMessage = err
        })
    }

    parseLog(log:ILog):ISingleLog{
        return this.createSingleLog(log, log.logExercises[0]);
      }
    
    createSingleLog(log:ILog, exerciseLog:ILogExercise) :ISingleLog {
        let tmpLog:ISingleLog = {
          logId : log.logId,
          user : log.user,
          set : log.set,
          comments : log.comments,
          created : log.created,
          exerciseId : exerciseLog.exerciseId,
          exerciseName : exerciseLog.exerciseName,
          reps : exerciseLog.reps,
          targetRep : exerciseLog.targetRep,
          weight : exerciseLog.weight     
        }
    
        return tmpLog;
    }

    editLog($event){
        let log:ISaveLog = this.createSaveLog($event);
        this.logService.updateLog(log).subscribe( (data: ILog) =>{
            console.log("update successful")
        },
            (error: any) => console.log(error)
        )
    }

    createSaveLog(log: ISingleLog) : ISaveLog{
        const tmplog:ISaveLog = {
            logId:log.logId,
            user :log.user,
            set:log.set, 
            comments:log.comments, 
            created:log.created,
            logExercise: this.createLogExercise(log)
        }

        return tmplog;
    }

    createLogExercise(log: ISingleLog):ILogExercise{
        const tmpLogExercise:ILogExercise = {
            logId : log.logId,
            exerciseId : log.exerciseId,
            exerciseName: log.exerciseName,
            reps: log.reps,
            weight: log.weight,
            targetRep: log.targetRep
        }

        return tmpLogExercise
    }
}