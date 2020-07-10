import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';

import { AuthService } from '../core/services/authService';
import { TOKEN, LOGGED_IN_USER, TOKEN_EXPIRATION } from '../shared/constants';
import { IUserAccount, IJWTToken, IResetPassword } from '../shared/models/user';

export function getToken():any{
    return localStorage.getItem(TOKEN);
}

@Injectable({
    providedIn: 'root'
})
export class UserStore{
    
    private _isLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject(this.isLoggedIn());
    public isLoggedIn$: Observable<boolean> = this._isLoggedIn.asObservable();
    
    constructor(private authService:AuthService, private jwtHelper: JwtHelperService){}

    public isAuthorized():boolean{
        return true;
    }

    public isLoggedIn():boolean{
        const token = getToken();
        return token != null && !this.jwtHelper.isTokenExpired(token);
    }

    public login(account){
        this.authService.login(account)
        .pipe(tap({
            next:response => {
                this.setToken(response)
            },
            error:err => {
                this.clearToken();
            }
        }));
    }

    public logout(){
        this.authService.logout()
        .pipe(tap({
            next:() => {
                this.clearToken();
            },
            error:err => {
                this.clearToken();
            }
        }));
    }

    public getLoggedInUser(){
        return localStorage.getItem(LOGGED_IN_USER);
    }

    public resetPassword( resetPassword: IResetPassword ) {
        return this.authService.resetPassword(resetPassword).pipe(tap( (token:IJWTToken) => {
          this.setToken(token);
        }));
      }

    private clearToken(){
        localStorage.removeItem(TOKEN);
        localStorage.removeItem(LOGGED_IN_USER);
        this._isLoggedIn.next(false);
    }

    private setToken(token:IJWTToken){
        localStorage.setItem(TOKEN, token.token);      
        localStorage.setItem(LOGGED_IN_USER, token.userName);
        this._isLoggedIn.next(true);
    }

    register(account:IUserAccount){
        return this.authService.register(account);
    }
}