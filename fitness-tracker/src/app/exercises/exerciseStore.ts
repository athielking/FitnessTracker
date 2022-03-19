import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Injectable } from '@angular/core';

import { ExerciseService } from './exercise.service';
import { Exercise } from '../shared/models/exercise';

@Injectable({
    providedIn: 'root'
})
export class ExerciseStore{
    
    private _exerciseList: BehaviorSubject<Exercise[]> = new BehaviorSubject(null);
    public exerciseList$:Observable<Exercise[]> = this._exerciseList.asObservable();

    constructor(private exerciseService:ExerciseService){}

    public getExercises():Observable<Exercise[]>{
        return this.exerciseService.getExercises()
            .pipe(tap({
                next:(exercises:Exercise[]) => {
                    this._exerciseList.next(exercises)
                }
            }));
    }

    public getExercise(id){
        return this._exerciseList.value.filter( x => x.exerciseId == id)[0];     
    }
}