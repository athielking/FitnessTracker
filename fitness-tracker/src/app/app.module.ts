import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'
import { CookieService } from 'ngx-cookie-service'

import { AppComponent } from "./app.component";
import { WelcomeComponent } from './home/welcome.component';
import { LogsModule } from './logs/logs.module';
import { ToastrModule } from 'ngx-toastr';
import { UsersModule } from './users/users.module';

import { AuthAccountLoginComponent } from './auth/auth-account/auth-account-login.component';
import { AuthAccountRegisterComponent } from './auth/auth-account/auth-account-register.component';
import { CoreModule } from './core/core.module';

import { MsalAuthModule } from './msal/msal.module';
import { MSALAuthguard } from './msal/msal-authguard';
import { MsalGuard, MsalRedirectComponent } from '@azure/msal-angular';

const appRoute: Routes = [
    {path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule), canLoad: [MSALAuthguard, MsalGuard]},
    {path: 'logs', loadChildren: () => import('./logs/logs.module').then(m => m.LogsModule), canLoad: [MSALAuthguard, MsalGuard]},
    //{path: 'login', component: AuthAccountLoginComponent},
    {path: 'register', component: AuthAccountRegisterComponent},
    {path: 'welcome', component: WelcomeComponent},
    {path: 'auth', component: MsalRedirectComponent },
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
    UsersModule,
    LogsModule,
    CoreModule,
    MsalAuthModule.forRoot(),
    ToastrModule.forRoot(), 
    RouterModule.forRoot(appRoute),
  ],
  providers: [
    MSALAuthguard,
    MsalGuard,
    CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
