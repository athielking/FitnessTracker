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
    
    /*intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const idToken = this.authStore.getToken();

        if (idToken) {
            const cloned = req.clone({
                setHeaders:{"Authorization" : `Bearer " ${idToken}`}
            })

            return next.handle(cloned)
        }
        else {
            // return to server unmodifed
            return next.handle(req)
            .pipe(
                retry(1),
                catchError( (error: HttpErrorResponse) => {
                    
                }) 
            )
        }
    }*/

    
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    const idToken = this.authStore.getToken();

        if (idToken) {
            const cloned = req.clone({
                setHeaders:{"Authorization" : `Bearer " ${idToken}`}
            })

            req = cloned;
        }
 
    return next.handle(req)
      .pipe(
        retry(1),
        catchError((error: HttpErrorResponse) => {
          let errorMessage = '';
          if (error.error instanceof ErrorEvent) {
            // client-side error
            errorMessage = `Error: ${error.error.message}`;
          } else {
            // server-side error
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
          }
          window.alert(errorMessage);
          return throwError(errorMessage);
        })
      )
  }
}