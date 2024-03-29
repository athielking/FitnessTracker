import { Injectable } from "@angular/core"

import { HttpClientService } from '../core/services/httpclient.service'
import { Observable, BehaviorSubject } from 'rxjs'
import { map, tap, takeUntil } from 'rxjs/operators'
import { IUser, User } from '../shared/models/user'

const path:string = "https://localhost:5001/api/user"

@Injectable({
    providedIn: 'root'
})
export class UserService{
    
    constructor(private httpClientService:HttpClientService){
    }
    
    getUsers():Observable<IUser[]>{
        return this.httpClientService.get<IUser[]>(path)
        .pipe(map( (users:IUser[]) =>{
            return users.map(user => new User(user))
        }))
    }

    getUserById(id:string){
        return this.httpClientService.get<IUser>(`${path}/GetById/${id}`)
        .pipe(map( (log:IUser) =>{
            return new User(log);
        }))
    }

    updateLog(user:IUser):Observable<IUser>{
      return this.httpClientService.put<IUser>(`${path}/${user.userName}`, user);
    }

    deleteUser(userName:string){
        return this.httpClientService.delete(`${path}/${userName}`);
    }
}