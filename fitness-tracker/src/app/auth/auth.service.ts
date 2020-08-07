import { HttpClientService } from '../core/services/httpclient.service';
import { Injectable } from '@angular/core';
import { shareReplay } from 'rxjs/operators';

import { IUserAccount } from 'src/app/shared/models/user';
import { IResetPassword } from '../shared/models/security';

const path:string = "http://localhost/FitnessTracker.Api/api/auth"

@Injectable({
    providedIn: 'root'
})
export class AuthService{
    
    user:IUserAccount;

    token:string;
    expiration:number;
 
    constructor(private httpClientService:HttpClientService){}

    login(account){
        account.reMemberMe = false;
        return this.httpClientService.post(`${path}/Login`, account).pipe(shareReplay());
    }

    logout(){
        return this.httpClientService.post(`${path}/Logout`, {}).pipe(shareReplay());
    }

    forgotUsername(account){
        return this.httpClientService.post(`${path}/Logout`, account).pipe(shareReplay());
    }

    resfreshToken(){
        return this.httpClientService.post(`${path}/RefreshToken`, null).pipe(shareReplay());
    }

    resetPassword(resetPassword: IResetPassword){
        return this.httpClientService.post(`${path}/ResetPassword`, resetPassword).pipe(shareReplay());
    }

    register(account:IUserAccount){
        return this.httpClientService.post(`${path}/Register`, account).pipe(shareReplay());
    }
}