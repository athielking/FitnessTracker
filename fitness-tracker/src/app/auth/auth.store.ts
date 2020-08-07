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
        const token = this.getToken(JWT_TOKEN);
        const tokenExpiration = this.getTtokenExpiration(TOKEN_EXPIRATION);
        var x = !(token === null || token.length == 0 || tokenExpiration === null || tokenExpiration < new Date());
        return !(token === null || token.length == 0 || tokenExpiration === null || tokenExpiration < new Date());
    }

    public login(account){
        return this.authService.login(account)
        .pipe(tap({
            next:(response:IJWTToken) => {
                this.setToken(this.parseJWTToken(response))
                this.setUser(response.userID);
                
            },
            error:err => {
                this.clearToken();
            }
        }));
    }

    private parseJWTToken(token:IJWTToken):Map<string, string>{
        const jwt:Map<string, string> = new Map([
            [JWT_TOKEN, token.token],
            [TOKEN_EXPIRATION, token.expiration]
        ]);

        return jwt;
    }

    public logout(){
        return this.authService.logout().subscribe(
            {
                next:(response:IJWTToken) => {
                    this.clearToken();
            },
                error:err => {
                    this.clearToken();
            }
        })       
    }

    public getToken(key:string = JWT_TOKEN){
        //return this.cookieService.get(JWT_TOKEN);
        return localStorage.getItem(key);
    }

    public getTtokenExpiration(key:string){
        return new Date(localStorage.getItem(key));
    }

    public refreshToken(){
        return this.authService.resfreshToken()
        .pipe(tap((token:IJWTToken) => {
            this.setToken(this.parseJWTToken(token));
        }))
    }

    public resetPassword( resetPassword: IResetPassword ) {
        return this.authService.resetPassword(resetPassword).pipe(tap( (token:IJWTToken) => {
          this.setToken(this.parseJWTToken(token));
        }));
      }

    public register(account){
        return this.authService.register(account);
    }

    private clearToken(){
        //this.cookieService.deleteAll(JWT_TOKEN);
        localStorage.removeItem(JWT_TOKEN);
        localStorage.removeItem(TOKEN_EXPIRATION);
        this._loggedInUser.next(null);
        this._isLoggedIn.next(false);
    }

    private setUser(id:string){
        this.userService.getUserById(id).subscribe( (user:IUser) => {
            this._loggedInUser.next(user) 
        })
    }

    private setToken(tokenMapping:Map<string, string>){ 
        //this.cookieService.set(JWT_TOKEN, token.token, +token.expiration, "/", ".http://localhost:4200", true, true);
        //this.cookieService.set(JWT_TOKEN, token.token, +token.expiration);
        
        for(let [key, value] of tokenMapping){
            if(key){
                localStorage.setItem(key, value);
            }
        }

        this._isLoggedIn.next(true);
    }
}