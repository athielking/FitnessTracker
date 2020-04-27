import { Component, OnInit } from "@angular/core"
import {ILog} from "./log"
import { LogService } from './log-list.service';
import { FormGroup, FormControl } from '@angular/forms';


@Component({
  templateUrl: "./log-list.component.html",
  styleUrls: ["./log-list.component.css"]
})


export class LogListComponent implements OnInit { 
  
  filteredLogs: ILog[] = []; 
  logs: ILog[] = [];
  testlogs: ILog[] = []; 

  logForm:FormGroup

  pageTitle:string = "Log List"
  logsFilter:string = "";
  errorMessage:string = "";

  constructor(private logService:LogService) { }

  performFilter(filterBy:string) : ILog[]{
    filterBy = filterBy.toLocaleLowerCase();

    return this.logs.filter((log : ILog) => 
      log.user.username.toLocaleLowerCase().indexOf(filterBy) !== -1)
  }

  ngOnInit(){
    
    this.initFormGroup()
    
    this.logService.getLogs().subscribe( {
      next:(logs:ILog[]) => {
        this.logs = logs 
        this.filteredLogs = this.logs;
      },
      error:err => this.errorMessage = err
    });
  }

  logFilterChanges(value){
    this.logsFilter = value
    this.filteredLogs = this.logsFilter ? this.performFilter(this.logsFilter) : this.logs
  }

  getTestLogs(){
    this.logService.getLogs().subscribe( {
      next:(logs:ILog[]) => {
        this.testlogs = this.logs;
      },
      error:err => this.errorMessage = err
    });
  }

  private initFormGroup(){
    this.logForm = new FormGroup({
      logFilter: new FormControl()
    });

    this.logForm.get('logFilter').valueChanges.subscribe( value => {
      this.logFilterChanges(value)
    })
  }
}
