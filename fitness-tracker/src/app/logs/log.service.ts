import { Injectable } from "@angular/core"

import { ILog, ISaveLog} from "./log"
import { HttpClientService } from '../core/services/httpclient.service'
import { Observable } from 'rxjs'

const path:string = "http://localhost:5001/api/log"

@Injectable({
    providedIn: 'root'
})
export class LogService{

    constructor(private httpCleintService:HttpClientService){
    }
    
    getLogs(){
      return this.httpCleintService.get<ILog[]>(path)
    }

    getLogByid(id){
      return this.httpCleintService.get<ILog>(`${path}/${id}`)
    }

    updateLog(log:ISaveLog):Observable<ILog>{
      return this.httpCleintService.put<ISaveLog>(`${path}/${log.logId}`, log);
    }
}