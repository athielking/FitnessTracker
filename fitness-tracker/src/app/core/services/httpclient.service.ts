import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError, Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class HttpClientService{

  httpOptions = {
    //headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*'})
    headers: new HttpHeaders({ 'Content-Type': 'application/json'})
  };

  constructor(private http:HttpClient){}

  get<T>(path):Observable<T>{
    return this.http.get<T>(path).pipe(
      catchError(this.handleError)
    );
  }

  put<T>(path, obj:T): Observable<any>{
    return this.http.put(path, obj, this.httpOptions)
    .pipe(catchError(this.handleError))      
  }

  post<T>(path, obj:T): Observable<any>{
    return this.http.post(path, obj, this.httpOptions)
    .pipe(catchError(this.handleError))    
  }

  delete(path): Observable<any>{
      return this.http.delete(path);
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