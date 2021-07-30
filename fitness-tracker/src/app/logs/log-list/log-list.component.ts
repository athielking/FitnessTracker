import { Component, OnInit,OnDestroy} from "@angular/core"
import { Router } from '@angular/router';
import { Observable, Subject} from 'rxjs';

import {ILog} from "../../shared/models/log"
import { LogService } from '../log.service';
import { AuthStore } from "src/app/auth";
import { IUser } from "src/app/shared/models/user";

@Component({
  templateUrl: "./log-list.component.html",
  styleUrls: ["../log.css"]
})
export class LogListComponent implements OnInit,OnDestroy { 
  
    logs$: Observable<ILog[]>  

    user:IUser;
    user$:any;
    pageTitle:string = "Log List"

    logs:ILog[];

    private _onDestroy$ = new Subject();

    constructor(private authStore: AuthStore, private logService:LogService, private route: Router) { }
    
    ngOnDestroy(): void {
      if(this._onDestroy$){
          this._onDestroy$.next(null);
          this._onDestroy$.unsubscribe();
      }
    }

    ngOnInit(){

        this.user$ = this.authStore.loggedInUser$.subscribe((user:IUser) =>{
          this.user = user;
        })

        this.logs$ = this.logService.getUserLogs(this.user.id);
    }

    createSet(log:ILog){
      this.route.navigate(['/logs-add', {id:log.logId, date:log.created}]) 
    }
  
}
