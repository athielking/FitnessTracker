import { BrowserModule } from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'

import { AppComponent } from "./app.component";
import { HttpClientModule } from '@angular/common/http';
import { WelcomeComponent } from './home/welcome.component';
import { LogsModule } from './logs/logs.module';

const appRoute: Routes = [
    {path: 'logs', redirectTo: '/logs', pathMatch: 'full'},
    {path: 'welcome', component: WelcomeComponent},
    {path: '', redirectTo: 'welcome', pathMatch: 'full'},
    {path: '**', redirectTo: 'welcome', pathMatch: 'full'}
]

@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent
  ],
  imports: [
    BrowserModule,
    //FormsModule,
    ReactiveFormsModule,   
    HttpClientModule,
    LogsModule,
    RouterModule.forRoot(appRoute)

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
