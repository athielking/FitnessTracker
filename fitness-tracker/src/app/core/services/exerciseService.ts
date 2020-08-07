import { HttpClientService } from './httpclient.service';
import { Injectable } from '@angular/core';
import { shareReplay, map } from 'rxjs/operators';
import { IExercise, createExercise } from 'src/app/shared/models/exercise';


const path:string = "http://localhost/FitnessTracker.Api/api/exercise"

@Injectable({
    providedIn: 'root'
})
export class ExerciseService{
    constructor(private httpClientService:HttpClientService){}

    getExercises(){
        return this.httpClientService.get<IExercise[]>(path).pipe(shareReplay());
    }
}