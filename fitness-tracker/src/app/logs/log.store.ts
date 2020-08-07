import { Injectable } from "@angular/core"

import { ILog, ISaveLog} from "../shared/models/log"

import { Observable, BehaviorSubject } from 'rxjs'
import { map, tap, takeUntil } from 'rxjs/operators'
import { LogService } from './log.service';
import { IExercise } from '../shared/models/exercise';

export class LogStore{
    
    private _logList:BehaviorSubject<ILog[]> = new BehaviorSubject(null);
    public logList$: Observable<ILog[]> = this._logList.asObservable();

    private _getLog: BehaviorSubject<ILog> = new BehaviorSubject(null);
    public log$: Observable<ILog> = this._getLog.asObservable();

    constructor(private logService:LogService, private exerciseService){}

    public getLogs(){
        //this._logsLoading.next()
        return this.logService.getLogs()
            .pipe(tap({
                next:logs => this._logList.next(logs)  
            }))
    }

}