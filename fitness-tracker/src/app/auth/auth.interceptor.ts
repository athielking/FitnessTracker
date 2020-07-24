import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

import { AuthStore } from './auth.store';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {

    constructor(private authStore:AuthStore){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        const idToken = this.authStore.getToken();
        
        const request = req.clone({
            headers: req.headers.append('Authorization', `Bearer ${idToken}`)
        });

        return next.handle(request);
    }
}