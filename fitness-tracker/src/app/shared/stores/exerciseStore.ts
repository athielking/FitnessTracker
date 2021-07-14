import { BehaviorSubject, Observable } from 'rxjs';
import { IExercise } from 'src/app/shared/models/exercise';
import { tap } from 'rxjs/operators';
import { ExerciseService } from '../../core/services/exerciseService';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class ExerciseStore{
    
    private _exerciseList: BehaviorSubject<IExercise[]> = new BehaviorSubject(null);
    public exerciseList$:Observable<IExercise[]> = this._exerciseList.asObservable();

    constructor(private exerciseService:ExerciseService){}

    public getExercises():Observable<IExercise[]>{
        return this.exerciseService.getExercises()
            .pipe(tap({
                next:(exercises:IExercise[]) => {
                    this._exerciseList.next(exercises)
                }
            }));
    }

    public getExercise(id){
        return this._exerciseList.value.filter( x => x.exerciseId == id)[0];     
    }
}