import { BrowserModule } from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from "@auth0/angular-jwt";

import { AppComponent } from "./app.component";
import { HttpClientModule } from '@angular/common/http';
import { WelcomeComponent } from './home/welcome.component';
import { LogsModule } from './logs/logs.module';
import { ToastrModule } from 'ngx-toastr';
import { UsersModule } from './users/users.module';
import { getToken } from './users/user.store';

const appRoute: Routes = [
    {path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule)},
    {path: 'logs', loadChildren: () => import('./logs/logs.module').then(m => m.LogsModule)},
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
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,   
    HttpClientModule,
    UsersModule,
    LogsModule,
    ToastrModule.forRoot(), 
    RouterModule.forRoot(appRoute),
    JwtModule.forRoot({
      config: {
        tokenGetter: getToken,
        whitelistedDomains: ["localhost:5001"],
        blacklistedRoutes: []
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
