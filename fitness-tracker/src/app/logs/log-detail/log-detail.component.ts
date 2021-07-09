import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable, Subject } from 'rxjs';

import { NotificationService } from 'src/app/core/services/notification.service';
import {ILog, ILogExercise} from '../../shared/models/log'
import { LogService } from '../log.service';

@Component({
  selector: 'log-detail',
  templateUrl: './log-detail.component.html',
  styleUrls: ['../log.css']
})
export class LogDetailComponent implements OnInit, OnDestroy{
  
    @Input() log:ILog
    filteredLogs$: Observable<ILog[]>

    logExercise:ILogExercise[]

    private _onDestroy$ = new Subject();

    isHidden:boolean = true;

    constructor(private logService:LogService, private notifyService : NotificationService,private route: Router, private activatedRoute: ActivatedRoute) { }

  
    ngOnInit(): void {
      let id:string = undefined;
      let date:string = "";
      this.activatedRoute.paramMap.subscribe( (params: ParamMap) =>{
          id = params.get('id');
          date = params.get('date');
          id = id ? id : this.log.logId.toString();
          date = date ? date : this.log.created;
      })
      
      this.filteredLogs$ = this.logService.getLogBySet(this.log.setId, date);
    }

    ngOnDestroy(): void {
        if(this._onDestroy$){
            this._onDestroy$.next(null);
            this._onDestroy$.unsubscribe();
        }
    }

    edit(id:number){
        this.route.navigate(['/logs-edit', id])
    }

    delete(id:number){
        if (confirm("Are You Sure You want to delete this Log")) {
            this.logService.deleteLog(id).subscribe( () =>{
                this.notifyService.showSuccess("Log deleted");
            }, error => {
              this.notifyService.showError("Log deleted");
            })
        }
    }
}
