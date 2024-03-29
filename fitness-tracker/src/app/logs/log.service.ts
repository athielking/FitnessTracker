import { Injectable } from "@angular/core"
import { Observable, BehaviorSubject } from 'rxjs'
import { map, shareReplay } from 'rxjs/operators'

import { HttpClientService } from '../core/services/httpclient.service'
import { ILog, ISaveLog, Log} from "../shared/models/log"

const path:string = "https://localhost:5001/api/log"

@Injectable({
    providedIn: 'root'
})
export class LogService{

    private _getLogs: BehaviorSubject<ILog[]> = new BehaviorSubject(null);
    public logList$: Observable<ILog[]> = this._getLogs.asObservable();

    private _getLog: BehaviorSubject<ILog> = new BehaviorSubject(null);
    public log$: Observable<ILog> = this._getLog.asObservable();
  
    constructor(private httpClientService:HttpClientService){
    }

    getLogs():Observable<ILog[]>{
        return this.httpClientService.get<ILog[]>(path)
        .pipe(map( (logs:ILog[]) =>{
            return logs.map( log => new Log(log))
        }));
    }

    getUserLogs(id:string):Observable<ILog[]>{
        return this.httpClientService.get<ILog[]>(`${path}/GetUserLogs/${id}`)
        .pipe(map( (logs:ILog[]) =>{
            return logs.map( log => new Log(log))
        }));
    }

    getLogById(id:string){
        return this.httpClientService.get<ILog>(`${path}/${id}`)
        .pipe(map( (log:ILog) =>{
            return new Log(log);
        }));
    }

    getLogBySet(id:string, date:string):Observable<ILog[]>{
        return this.httpClientService.get<ILog[]>(`${path}/${id}/${date}`)
        .pipe(map ( (logs:ILog[]) => {
            return logs.map( log => new Log(log));
        }));
    }

    createLog(log:ISaveLog):Observable<ILog[]>{
        return this.httpClientService.post<ISaveLog>(`${path}`, log).pipe(shareReplay())
    }

    updateLog(log:ISaveLog):Observable<ILog>{
      return this.httpClientService.put<ISaveLog>(`${path}/${log.logId}`, log).pipe(shareReplay())
    }

    addLog(log:ISaveLog):Observable<ILog>{
        return this.httpClientService.post<ISaveLog>(`${path}`, log).pipe(shareReplay())
    }

    deleteLog(id:number){
        return this.httpClientService.delete(`${path}/${id}`).pipe(shareReplay())
    }
}