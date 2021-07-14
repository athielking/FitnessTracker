import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import { Observable } from 'rxjs';

import { AuthStore } from './auth.store';

@Injectable({
    providedIn: 'root'
  })
  export class AuthGuard implements CanActivate {
    
    constructor(private router: Router, private authStore:AuthStore){}

    canActivate(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

        const isAuthorized:boolean = this.authStore.isAuthorized();

        if(!isAuthorized){
            this.router.navigate(['/user/login']);
        }

        return isAuthorized;
    }
  }