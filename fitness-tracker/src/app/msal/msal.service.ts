import { Inject, Injectable } from "@angular/core";
import { MsalService } from "@azure/msal-angular";
import { AuthenticationResult } from "@azure/msal-browser";
import * as MicrosoftGraph from "@microsoft/microsoft-graph-types";
import { BehaviorSubject, Observable } from "rxjs";
import { MICROSOFT_GRAPH_SETTINGS } from "../core/services/appsettings-services";

import { HttpClientService } from "../core/services/httpclient.service";
import { MicrosoftGraphSettings } from "./models/microsoftgraph-settings";

interface IODataResult<T> {
    value: T;
}

@Injectable({
    providedIn: 'root'
})
export class MsalAuthService{
    private _loggedIn: BehaviorSubject<boolean> = new BehaviorSubject(null);
    public loggedIn$: Observable<boolean> = this._loggedIn.asObservable();

    redirectUrl:string;

    constructor(
        private authService: MsalService, 
        @Inject(MICROSOFT_GRAPH_SETTINGS)private microsoftGraphSettings: MicrosoftGraphSettings, 
        private httpClientService:HttpClientService) {}

    checkAccount() {
        this._loggedIn.next(this.authService.instance.getAllAccounts().length > 0);
    }

    login() {
        this.authService
          .loginPopup()
          .subscribe((response: AuthenticationResult) => {
            this.authService.instance.setActiveAccount(response.account);
            this.checkAccount();
          });
    }
    
    logout() {
        this.authService.logout();
        this._loggedIn.next(false);
    }

    getProfile() {
        return this.httpClientService.get<MicrosoftGraph.User>(`${this.microsoftGraphSettings.baseAddress}/v1.0/me`);
    }
    
    getUsers(params):any {
        let url = `https://graph.microsoft.com/v1.0/users?${params.toString()}`;
        
        return this.httpClientService
          .get<IODataResult<MicrosoftGraph.User[]>>(url);
    }
}