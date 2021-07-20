import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, Router, RouterStateSnapshot, UrlSegment} from '@angular/router';
import { Observable } from 'rxjs';

import { AuthStore } from './auth.store';

@Injectable({
    providedIn: 'root'
  })
  export class AuthGuard implements CanLoad {
    
    constructor(private router: Router, private authStore:AuthStore){}

    canLoad(route: Route, segments: UrlSegment[]): boolean | Promise<boolean> | Observable<boolean> {
      const isAuthorized:boolean = this.authStore.isAuthorized();

        if(!isAuthorized){
            this.router.navigate(['/login']);
        }

        return isAuthorized;
    }

    canActivate(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

        const isAuthorized:boolean = this.authStore.isAuthorized();

        if(!isAuthorized){
            this.router.navigate(['/login']);
        }

        return isAuthorized;
    }
  }