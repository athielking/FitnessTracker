import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import {ExercisesComponent} from "./exercises.component"
import { ExercisesRoutingModule } from "./exercises.routing.module";

@NgModule({
    declarations:[
        ExercisesComponent     
    ],
    imports:[
        CommonModule,
        ExercisesRoutingModule,
    ]
})
export class ExercisesModule {}