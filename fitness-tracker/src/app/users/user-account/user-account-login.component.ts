import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserStore } from '../user.store';
import { NotificationService } from 'src/app/core/services/notification.service';
import { createUserAccount } from 'src/app/shared/models/user';

@Component({
    templateUrl: './user-account-login.component.html',
    styleUrls: ['../user.css']
})
export class UserAccountLoginComponent implements OnInit{

    loginForm:FormGroup  

    formErrors = {
        'userName':'',
        'password':'',
    }

  
    constructor(private router: Router, private formBuilder: FormBuilder, private userStore: UserStore, private notifyService : NotificationService){

    }

    ngOnInit(): void {
        this.initFormGroup();
    }

    login(){
        if(this.loginForm.invalid) return

        this.userStore.login(this.loginForm.value);
    }

    private initFormGroup(){
        this.loginForm = this.formBuilder.group({
            userName:['', [Validators.required]],
            password:['', [Validators.required]],
            reMemberMe:''
        })
    }

    /*
    login(form: NgForm) {
    const credentials = JSON.stringify(form.value);
    this.http.post("http://localhost:5000/api/auth/login", credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      const token = (<any>response).token;
      localStorage.setItem("jwt", token);
      this.invalidLogin = false;
      this.router.navigate(["/"]);
    }, err => {
      this.invalidLogin = true;
    });
  }


   logOut() {
    localStorage.removeItem("jwt");
    }    
    
    */
}