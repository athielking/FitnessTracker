import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { Exercise } from "../shared/models/exercise";
import { ExerciseStore } from "./exerciseStore";

@Component({
    templateUrl: './exercises.component.html'
})
export class ExercisesComponent implements OnInit{
    pageTitle:string = 'Exercises';

    exercises$?: Observable<Exercise[]>;

    constructor(private exerciseStore:ExerciseStore){}

    ngOnInit(): void {
      console.log("ExercisesComponent.ngOnInit");

      this.exercises$ = this.exerciseStore.getExercises()
    }
}