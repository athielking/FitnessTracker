import { Injectable } from "@angular/core"
import { Observable, BehaviorSubject } from 'rxjs'
import { map } from 'rxjs/operators'

import { HttpClientService } from '../core/services/httpclient.service'
import { ILog, ISaveLog, createLog} from "../shared/models/log"

const path:string = "http://localhost:5001/api/log"

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
            return logs.map( log => createLog(log))
        }));
    }

    getLogById(id:string){
        return this.httpClientService.get<ILog>(`${path}/${id}`)
        .pipe(map( (log:ILog) =>{
            return createLog(log);
        }));
    }

    getLogBySet(id:string, date:string):Observable<ILog[]>{
        return this.httpClientService.get<ILog[]>(`${path}/${id}/${date}`)
        .pipe(map ( (logs:ILog[]) => {
            return logs.map( log => createLog(log));
        }));
    }

    updateLog(log:ISaveLog):Observable<ILog>{
      return this.httpClientService.put<ISaveLog>(`${path}/${log.logId}`, log);
    }

    addLog(log:ISaveLog):Observable<ILog>{
        return this.httpClientService.post<ISaveLog>(`${path}`, log);
    }

    deleteLog(id:number){
        return this.httpClientService.delete(`${path}/${id}`);
    }
}