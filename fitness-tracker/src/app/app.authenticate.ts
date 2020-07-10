import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import { Observable } from 'rxjs';

import { UserStore } from './users/user.store';

@Injectable({
    providedIn: 'root'
  })
  export class AuthorizationStep implements CanActivate {
    
    constructor(private router: Router, private userStore:UserStore){

    }

    canActivate(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        const roles = childRoute.data.roles;
        const isAuthorized:boolean = this.userStore.isAuthorized();

        if(!isAuthorized){
            this.router.navigate(['/user/login']);
        }

        return isAuthorized;
    }
  }