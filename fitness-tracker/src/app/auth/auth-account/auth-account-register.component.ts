import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { Router } from '@angular/router';

import { IUserAccount } from 'src/app/shared/models/user';
import { NotificationService } from 'src/app/core/services/notification.service';
import { propertyLengthGuard, passwordGuard, confirmedValidator } from 'src/app/shared/custom-validators';
import { AuthStore } from '../auth.store';

let validationMessages = {
    'firstName':{
        'minLength':'First Name must be greater than 1 character'
    },
    'lastName':{
        'minLength':'Last Name must be greater than 1 character'
    },
    'email' :{
        'required':'Email is required',
        'email':'Email is not valid'
    },
    'userName' :{
        'required':'Username is required',
    },
    'password' :{
        'required':'Password is required',
        'passwordStrength':'Password must be atleast 8 characters long.  Must have uppercase, lowercase, numbers and one special character'
    },
    'reEnterPassword' :{
        'required':'ReEnter Password is required',
        'confirmed':'Passwords must match'
    }
}
@Component({
    templateUrl: './auth-account-register.component.html',
        styleUrls: ['../auth.css']
    })

export class AuthAccountRegisterComponent implements OnInit{
  
    registerForm:FormGroup  
  
    formErrors = {
        'firstName':'',
        'lastName':'',
        'email':'',
        'userName':'',
        'password':'',
        'reEnterPassword':''
    }

    pageTitle = 'Register';

    constructor(private router: Router, private formBuilder: FormBuilder, private authStore: AuthStore, private notifyService : NotificationService){

    }

    ngOnInit(): void {
        this.initFormGroup();

        this.registerForm.valueChanges.subscribe((data) =>{
            this.logValidationErrors(this.registerForm);
        });
    }

    initFormGroup(){
        this.registerForm = this.formBuilder.group({
            firstName:['',[propertyLengthGuard(2, 'firstName')]],
            lastName:['',[propertyLengthGuard(2, 'lastName')]],
            email:['', [Validators.required, Validators.email]],
            userName:['', [Validators.required]],
            password:['', [Validators.required, passwordGuard(8)]],
            reEnterPassword:['', [Validators.required]]
        },
        {
            validator: confirmedValidator('password', 'reEnterPassword')
        });
    }

    logValidationErrors(group: FormGroup = this.registerForm): void{
        Object.keys(group.controls).forEach((key:string) => {
            const abstractControl = group.get(key);
            if(abstractControl instanceof FormGroup){
                this.logValidationErrors(abstractControl);
            }
            else{
                this.formErrors[key] = '';
                //if(abstractControl && !abstractControl.valid && (abstractControl.touched || abstractControl.dirty)){
                if(abstractControl && !abstractControl.valid){
                    const messages = validationMessages[key]; 
                    for (const errorKey in abstractControl.errors){
                        if(errorKey){
                            this.formErrors[key] += messages[errorKey] + ' ';
                            console.log(key + " : " +this.formErrors[key]);
                        }
                    }
                }
            }
        })
    }

    onSubmit(){
        if(this.registerForm.invalid) return
      
        const userAccount:IUserAccount = this.createUserAccount(this.registerForm)
        this.authStore.register(userAccount).subscribe( 
            () => {
                this.router.navigate(['/login']);
            },
            err => {
                if(err.status == 400){
                    this.handleServerError(JSON.parse(err.text))
                }else{
                    this.notifyService.showError("Unable to create User account");
                }
            });
    }

    private handleServerError(validationErrors : any){
        for(const propName in validationErrors){
            if(validationErrors.hasOwnProperty(propName)){
                if(this.registerForm.controls[propName]){
                    this.registerForm.controls[propName].setErrors( {invalid: true} )
                }
            }
        }
    }

    private createUserAccount(form: FormGroup) : IUserAccount{
        const userAccount:IUserAccount = {
            userName : form.value.userName,
            password : form.value.password,
            firstName: form.value.firstName,
            lastName : form.value.lastName,
            email : form.value.email
        }

        return userAccount;
    }

    
}

