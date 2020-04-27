import { Injectable } from "@angular/core"

import { ILog} from "./log"
import { HttpClientService } from '../core/services/httpclient.service'

const path:string = "http://localhost:5001/"

@Injectable({
  providedIn: 'root'
})
export class LogService{

  constructor(private httpCleintService:HttpClientService){
  }
  
  getLogs(){
    return this.httpCleintService.get<ILog[]>(path + "api/log")
  }

  getLogByid(id){
    return this.httpCleintService.get<ILog>(path + 'api/log/id=' + id)
  }
}