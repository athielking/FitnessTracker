import { HttpParams } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import * as MicrosoftGraph from "@microsoft/microsoft-graph-types";

import { MsalAuthService } from './msal/msal.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css', '../../node_modules/ngx-toastr/toastr.css']
})
export class AppComponent implements OnInit, OnDestroy{
  title:string = 'Fitness Tracker';
  
  profile$?: Observable<MicrosoftGraph.User>;
  users$?: Observable<MicrosoftGraph.User[]>;
  users?: MicrosoftGraph.User[];

  userNameFilter: string = "";

  private _onDestroy$ = new Subject();

  constructor(public msalService:MsalAuthService){}

  ngOnInit(): void {
    this.msalService.checkAccount();
  }

  ngOnDestroy(): void {
      if(this._onDestroy$){
        this._onDestroy$.unsubscribe();
      }
  }

  login(){
    this.msalService.login();
  }

  logout(){
    this.msalService.logout();
  }

  getProfile() {
    this.profile$ = this.msalService.getProfile();
  }

  getUsers() {
    let params = new HttpParams().set("$top", "10");
        if (this.userNameFilter) {
          params = params.set(
            "$filter",
            `startsWith(displayName, '${this.userNameFilter}')`
          );
        }
    
    this.msalService.getUsers(params)
      .pipe(takeUntil(this._onDestroy$))
      .subscribe((users) => (this.users = users.value));;
  }
}
