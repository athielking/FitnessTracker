import { Component, OnInit, Input, Output, EventEmitter, DoCheck } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

import { ILog, ISingleLog, ILogExercise, ISaveLog } from '../log';
import { LogService } from '../log.service';

@Component({
    selector:'log-form',
    templateUrl: './log-form.component.html',
    styleUrls: ['./log-form.component.css']
  })
  export class LogFormComponent implements OnInit, DoCheck {
    
    @Input() log:ISingleLog;
    @Output() editEvent = new EventEmitter<ISingleLog>();

    logForm:FormGroup  

    errorMessage:string = "";

    init:boolean;

    constructor(private route: ActivatedRoute, private router: Router, private logService:LogService, private formBuilder: FormBuilder) { 
        this.init = false;
    }
    
    ngOnInit(): void {
        this.initFormGroup();
    }

    ngDoCheck(){
        if(this.log && !this.init){
            this.editLog(this.log);
            this.init = true;
        }
    }

    private initFormGroup(){
        this.logForm = this.formBuilder.group({
            set:[''],
            weight:[''],
            reps:[''],
            targetRep:['']
        });
    }

    editLog(log:ISingleLog){
        this.logForm.setValue({
            set:log.set,
            weight:log.weight,
            reps:log.reps,
            targetRep:log.targetRep,
        });
    }

    onSubmit(){
        let updatedLog:ISingleLog = this.createSingleLog(this.log, this.logForm);
        this.editEvent.emit(updatedLog);
    }

    createSingleLog(log:ISingleLog, logform:FormGroup) :ISingleLog {
        let tmpLog:ISingleLog = {
          logId : log.logId,
          user : log.user,
          set : logform.value.set,
          comments : log.comments,
          created : log.created,
          exerciseId : log.exerciseId,
          exerciseName : log.exerciseName,
          reps : logform.value.reps,
          targetRep : logform.value.targetRep,
          weight : logform.value.weight     
        }
    
        return tmpLog;
    }

  }
 