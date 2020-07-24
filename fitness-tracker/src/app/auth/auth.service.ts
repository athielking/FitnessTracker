import { HttpClientService } from '../core/services/httpclient.service';
import { Injectable } from '@angular/core';
import { shareReplay } from 'rxjs/operators';

import { IUserAccount } from 'src/app/shared/models/user';
import { IResetPassword } from '../shared/models/security';

const path:string = "http://FitnessTracker.Api/api/auth"

@Injectable({
    providedIn: 'root'
})
export class AuthService{
    
    auth0 : any;
    user:IUserAccount;

    token:string;
    expiration:number;
 
    constructor(private httpClientService:HttpClientService){}

    login(account){
        account.reMemberMe = false;
        return this.httpClientService.post(`${path}/Login`, account);
    }

    logout(){
        return this.httpClientService.get(`${path}/Logout`).pipe(shareReplay());
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