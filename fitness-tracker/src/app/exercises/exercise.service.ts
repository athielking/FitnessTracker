import { HttpClientService } from '../core/services/httpclient.service';
import { Inject, Injectable } from '@angular/core';
import { shareReplay, map } from 'rxjs/operators';
import { Exercise } from '../shared/models/exercise';
import { APP_SERVER } from '../core/services/appsettings.services';

@Injectable({
    providedIn: 'root'
})
export class ExerciseService{
    constructor(private httpClientService:HttpClientService, @Inject(APP_SERVER)private appServer: string ){}

    getExercises(){
        return this.httpClientService.get<Exercise[]>(`${this.appServer}exercise`).pipe(shareReplay());
    }
}
