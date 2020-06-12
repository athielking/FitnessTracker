import { Component, OnInit, OnDestroy } from "@angular/core"
import {ILog} from "../../shared/models/log"
import { LogService } from '../log.service';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { Observable, combineLatest, Subject, of } from 'rxjs';
import { map, startWith, takeUntil, filter } from 'rxjs/operators';

import { ActivatedRoute, Params, ParamMap, Router } from '@angular/router';

@Component({
  templateUrl: "./log-list.component.html",
  styleUrls: ["../log.css"]
})
export class LogListComponent implements OnInit, OnDestroy { 
  
    logs$: Observable<ILog[]>  
    filteredLogs$: Observable<ILog[]>
    filter$: Observable<string> 
    
    filter:FormControl
    logForm:FormGroup

    pageTitle:string = "Log List"
    logsFilter:string = "";
    errorMessage:string = "";

    private _onDestroy$ = new Subject();

    constructor(private logService:LogService, private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private route: Router) { }

    ngOnDestroy(): void {
        if(this._onDestroy$){
            this._onDestroy$.next(null);
            this._onDestroy$.unsubscribe();
        }
    }

    ngOnInit(){

        let name = "";
        this.activatedRoute.paramMap.subscribe( (params: ParamMap) =>{
            name = params.get('name')
        })

        this.initFormGroup();
        this.logs$ = this.logService.getLogs();

        /*this.logs$ = this.logService.getLogs().pipe( map( (logs:ILog[]) => {
          return logs.filter(log => log.user.username.toLowerCase() === name && log.set == 1 )
        }));*/
        
        this.filter$ = this.logForm.get('logFilter').valueChanges.pipe(startWith(''));

        this.filteredLogs$ = combineLatest(this.logs$, this.filter$).pipe(takeUntil(this._onDestroy$),
            map(([logs, filterString]) => logs.filter(log => log.user.username.toLowerCase().indexOf
            (filterString.toLowerCase()) !== -1 && (log.set == 1)))
        );

        this.updateFilter(name);
    }

    private createLogList(logs$, filter$, username$){
        return combineLatest(
            logs$,
            filter$, (logs: ILog[], filter:string) =>{
                if(username$ = '') 
                  return logs
                return logs.filter(
                    (log: ILog) => {
                      log.user.username.toLowerCase() === filter.toLowerCase() && (log.set == 1)
                    }
                )
            }
        )
    }

    updateFilter(username:string){
        this.logForm.setValue({
          logFilter:username, emitEvent:true
        });
    }

    createSet(log:ILog){
      this.route.navigate(['/logs-add', {id:log.logId, date:log.created}]) 
    }

    private initFormGroup(){
      this.logForm = this.formBuilder.group({
          logFilter:[''],emitEvent:true
      });
  }
}
