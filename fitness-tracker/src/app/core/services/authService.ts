import { HttpClientService } from './httpclient.service';
import { Injectable } from '@angular/core';
import { IUserAccount, IResetPassword } from 'src/app/shared/models/user';

const path:string = "http://localhost:5001/api/auth"

@Injectable({
    providedIn: 'root'
})
export class AuthService{
    constructor(private httpClientService:HttpClientService){}

    login(account){
        account.reMemberMe = false;
        return this.httpClientService.post(`${path}/Login`, account);
    }

    logout(){
        return this.httpClientService.get(`${path}/Logout`);
    }

    forgotUsername(){

    }

    resetPassword(resetPassword: IResetPassword){
        return this.httpClientService.post(`${path}/ResetPassword`, resetPassword);
    }

    register(account:IUserAccount){
        return this.httpClientService.post(`${path}/Register`, account);
    }
}