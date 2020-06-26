import { Component } from '@angular/core';

@Component({
    templateUrl: './user-account-login.component.html',
    styleUrls: ['../user.css']
  })
export class UserAccountLoginComponent{

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