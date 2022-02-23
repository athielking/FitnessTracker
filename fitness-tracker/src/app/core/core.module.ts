import { HttpClientModule} from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from '@angular/router';
import { NavBarComponent } from "./nav-bar/nav-bar.component";

@NgModule({   
    declarations:[
        NavBarComponent
    ],
    imports:[
        BrowserModule, 
        RouterModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule
    ],
    exports:[
        BrowserModule, 
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        NavBarComponent
    ], 
})
export class CoreModule{}