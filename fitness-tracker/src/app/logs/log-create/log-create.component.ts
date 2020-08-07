import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Subject, Observable } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { LogService } from '../log.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { ILog, ILogExercise, ISaveLog, ISingleLog, createSaveLog, createSingleLog, } from 'src/app/shared/models/log';
import { IExercise } from 'src/app/shared/models/exercise';
import { AuthStore } from 'src/app/auth';
import { createUser, IUser } from 'src/app/shared/models/user';
import { ExerciseStore } from 'src/app/shared/stores/exerciseStore';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'log-create',
    templateUrl: './log-create.component.html',
    styleUrls: ['../log.css']
  })

export class LogCreateComponent implements OnInit, OnDestroy{
    
    exercises$: Observable<IExercise[]> 
    exercises: IExercise[]

    exerciseForm:FormGroup  
    
    log:ISingleLog

    ex:IExercise

    errorMessage:string = ""
    logCreatedOn:string
    exercise:IExercise;

    private _onDestroy$ = new Subject();
    
    constructor(
        private logService:LogService, 
        private notifyService : NotificationService,
        private route: Router, 
        private activatedRoute: ActivatedRoute,
        private authStore: AuthStore,
        private exerciseStore:ExerciseStore,
        private formBuilder: FormBuilder) { 

        }
    
    ngOnInit(): void {
        this.authStore.loggedInUser$.subscribe(user => {
            this.log = this.createSingleLog(user)
            this.log.created = new Date().toString()
        })
        
        //this.exercises$ = this.exerciseStore.getExercises();
        this.exerciseStore.getExercises().subscribe( exercises =>{
            this.exercises = exercises;
        })

        this.exerciseForm = this.formBuilder.group({
            exerciseType:[null, [ Validators.required, Validators.pattern('[0-9]')]]
        })

        this.exerciseForm.valueChanges.subscribe( exercise =>{
            this.log.exerciseId  = exercise.exerciseType
        })
    }

    ngOnDestroy(): void {
        throw new Error("Method not implemented.");
    }

    get exerciseType(){
        return this.exerciseForm.get('exerciseType');
    }

    saveLog(e){
        if(Number(this.log.exerciseId) > 0){
            let log:ISaveLog = createSaveLog(e);
            var exercise = this.exerciseStore.getExercise(this.log.exerciseId)
            log.logExercise.exerciseId = exercise.exerciseId;
            log.logExercise.exerciseName = exercise.name;

            this.logService.addLog(log).subscribe( (data: ILog) =>{
            this.notifyService.showSuccess("Log was Created");
            },
                (error: any) => {
                    console.log(error)
                    this.notifyService.showError("Failed to create log");
                }

            )
        }
    }

    createSingleLog(user:IUser) :ISingleLog {
        let tmpLog:ISingleLog = {
          logId : undefined,
          user : user,
          set : 1,
          comments : '',
          created : '',
          exerciseId : '',
          exerciseName : '',
          reps : 0,
          targetRep : 0,
          weight : 0    
        }
    
        return tmpLog;
    }

}