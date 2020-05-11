import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {ILog, ISingleLog, ILogExercise} from '../log'
import { LogService } from '../log.service';

@Component({
  templateUrl: './log-detail.component.html',
  styleUrls: ['./log-detail.component.css']
})
export class LogDetailComponent implements OnInit {

  logs:ISingleLog[] = []
  pageTitle:string = "Log Details: "
  errorMessage:string = ""
  
  constructor(private route: ActivatedRoute, private router: Router, private logService:LogService) { }

  ngOnInit(): void {
    let id = +this.route.snapshot.paramMap.get('id');
    this.logService.getLogByid<ILog[]>(id).subscribe({
      next:(logs:ILog[]) => {
        this.logs = this.parseLogs(logs)
        this.pageTitle += `: ${id}`;
      },
      error:err => this.errorMessage = err
    })
  }

  parseLogs(logs):ISingleLog[]{
    let tmpLogs:ISingleLog[] = []
    for(var log of logs){
      tmpLogs.push(this.createSingleLog(log, log.logExercises[0]))
    }

    return tmpLogs;
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

  onBack():void{
    this.router.navigate(['/logs']);
  }
}
