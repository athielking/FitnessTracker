import { BrowserModule } from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CookieService } from 'ngx-cookie-service'

import { AppComponent } from "./app.component";
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { WelcomeComponent } from './home/welcome.component';
import { LogsModule } from './logs/logs.module';
import { ToastrModule } from 'ngx-toastr';
import { UsersModule } from './users/users.module';

import { AuthGuard, AuthService } from './auth/index';

import { AuthAccountLoginComponent } from './auth/auth-account/auth-account-login.component';
import { HttpClientService } from './core/services/httpclient.service';
import { AuthInterceptor } from './auth/auth.interceptor';
import { AuthAccountRegisterComponent } from './auth/auth-account/auth-account-register.component';
import { CoreModule } from './core/core.module';

const appRoute: Routes = [
    {path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule), canLoad: [AuthGuard]},
    {path: 'logs', loadChildren: () => import('./logs/logs.module').then(m => m.LogsModule), canLoad: [AuthGuard]},
    {path: 'login', component: AuthAccountLoginComponent},
    {path: 'register', component: AuthAccountRegisterComponent},
    {path: 'welcome', component: WelcomeComponent},
    {path: '', redirectTo: 'welcome', pathMatch: 'full'},
    {path: '**', redirectTo: 'welcome', pathMatch: 'full'}
]

@NgModule({
  declarations: [
    AppComponent,
    AuthAccountLoginComponent,
    AuthAccountRegisterComponent,
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
    CoreModule,
    ToastrModule.forRoot(), 
    RouterModule.forRoot(appRoute),
  ],
  providers: [
    AuthGuard,
    AuthService,
    CookieService,
    HttpClientService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
