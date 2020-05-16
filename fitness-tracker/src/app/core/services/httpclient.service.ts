import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HttpClientService{

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http:HttpClient){}

  get<T>(path):Observable<T>{
    return this.http.get<T>(path).pipe(
      //tap(data => console.log('allL ' + JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  put<T>(path, obj:T): Observable<any>{
    return this.http.put(path, obj, this.httpOptions)
    .pipe(catchError(this.handleError))      
  }
  
  handleError(err: HttpErrorResponse){
    let errorMessage = '';
    if(err.error instanceof ErrorEvent){
      errorMessage = `An error occured: ${err.error.message}`
    }else{
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}