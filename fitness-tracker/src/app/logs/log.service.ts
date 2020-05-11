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
  
  getLogs<T>(){
    return this.httpCleintService.get<T>(path + "api/log")
  }

  getLogByid<T>(id){
    return this.httpCleintService.get<T>(path + 'api/log/' + id)
  }
}