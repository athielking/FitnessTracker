import { Component, OnInit, Input, Output, EventEmitter, DoCheck } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { ISingleLog} from '../../shared/models/log';
import { LogService } from '../log.service';

let validationMessages = {
    'set' :{
        'required':'Set is required'
    },
    'weight' :{
        'required':'Weight is required'
    },
    'reps' :{
        'required':'Reps is required'
    },
    'targetRep' :{
        'required':'Target Reps is required'
    }
}

@Component({
    selector:'log-form',
    templateUrl: './log-form.component.html',
    styleUrls: ['../log.css']
  })
  export class LogFormComponent implements OnInit, DoCheck {
    
    @Input() log:ISingleLog;
    @Output() editEvent = new EventEmitter<ISingleLog>();

    logForm:FormGroup  

    date:string = "";
    errorMessage:string = "";
    exerciseName:string = "";

    init:boolean;

    formErrors = {
        'set':'',
        'weight':'',
        'reps':'',
        'targetRep':''
    }

    constructor(private route: ActivatedRoute, private router: Router, private logService:LogService, private formBuilder: FormBuilder) { 
        this.init = false;
    }
    
    ngOnInit(): void {
        this.initFormGroup();

        this.logForm.valueChanges.subscribe((data) =>{
            this.logValidationErros(this.logForm);
        });
    }

    ngDoCheck(){
        if(this.log && !this.init){
            this.editLog(this.log);
            this.init = true;
            this.exerciseName = this.log.exerciseName;
            this.date = this.log.created;
        }
    }

    private initFormGroup(){
        this.logForm = this.formBuilder.group({
            set:['', [Validators.required]],
            weight:['', [Validators.required]],
            reps:['', [Validators.required]],
            targetRep:['', [Validators.required]]
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

    logValidationErros(group: FormGroup = this.logForm): void{
        Object.keys(group.controls).forEach((key:string) => {
            const abstractControl = group.get(key);
            if(abstractControl instanceof FormGroup){
                this.logValidationErros(abstractControl);
            }
            else{
                this.formErrors[key] = '';
                if(abstractControl && !abstractControl.valid){
                    const messages = validationMessages[key]; 
                    console.log(messages);
                    for (const errorKey in abstractControl.errors){
                        this.formErrors[key] += messages[errorKey] + ' ';
                    }
                }
            }
        })
    }

    onSubmit(){
        let updatedLog:ISingleLog = this.createSingleLog(this.log, this.logForm);
        this.editEvent.emit(updatedLog);
    }

    onBack(){
        this.router.navigate(['/logs-edit', {id:this.log.logId, date:this.log.created}])    
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
 