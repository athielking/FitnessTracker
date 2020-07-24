import { Injectable } from '@angular/core';
import { tap, catchError, mapTo } from 'rxjs/operators';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';

import { AuthService } from './auth.service';

import { JWT_TOKEN, LOGGED_IN_USER, TOKEN_EXPIRATION } from '../shared/constants';
import { IResetPassword, IJWTToken } from '../shared/models/security';
import { IUser} from '../shared/models/user';
import { UserService } from '../users/user.service';

let jwt:any = null

function getToken(){
    return jwt;
}

@Injectable({
    providedIn: 'root'
})
export class AuthStore{

    private _isLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject(this.isLoggedIn());
    public isLoggedIn$: Observable<boolean> = this._isLoggedIn.asObservable();

    private _loggedInUser: BehaviorSubject<IUser> = new BehaviorSubject(null);
    public loggedInUser$: Observable<IUser> = this._loggedInUser.asObservable();

    public constructor(
        private authService:AuthService, private cookieService:CookieService,private userService:UserService){}

    public isAuthorized():boolean{
        return this.isLoggedIn();
    }

    public isLoggedIn():boolean{
        const token = this.getToken();

        return !(token === null || token === "");
    }

    public login(account){
        return this.authService.login(account)
        .pipe(tap({
            next:(response:IJWTToken) => {
                this.setUser(response.userID);
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
            next:(response:IJWTToken) => {
                this.clearToken();
            },
            error:err => {
                this.clearToken();
            }
        }));          
    }

    public getToken(){
        return this.cookieService.get(JWT_TOKEN);
    }

    public refreshToken(){
        return this.authService.resfreshToken()
        .pipe(tap((token:IJWTToken) => {
            this.setToken(token);
        }))
    }

    public resetPassword( resetPassword: IResetPassword ) {
        return this.authService.resetPassword(resetPassword).pipe(tap( (token:IJWTToken) => {
          this.setToken(token);
        }));
      }

    public register(account){
        return this.authService.register(account);
    }

    private clearToken(){
        this.cookieService.deleteAll(JWT_TOKEN);
        this._loggedInUser.next(null);
        this._isLoggedIn.next(false);
    }

    private setUser(id:string){
        return this.userService.getUserById(id)
            .pipe(tap({
                next:user => this._loggedInUser.next(user)  
            }))
    }

    private setToken(token:IJWTToken){ 
        //this.cookieService.set(JWT_TOKEN, token.token, +token.expiration, "/", ".http://localhost:4200", true, true);
        this.cookieService.set(JWT_TOKEN, token.token, +token.expiration);
        this._isLoggedIn.next(true);
    }
}