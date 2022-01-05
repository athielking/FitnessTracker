import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { ModuleWithProviders, NgModule, Optional, SkipSelf } from "@angular/core";
import {} from "../core/core.module"

import {
    MsalBroadcastService,
    MsalGuard,
    MsalInterceptor,
    MsalModule,
    MsalService,
    MSAL_GUARD_CONFIG,
    MSAL_INSTANCE,
    MSAL_INTERCEPTOR_CONFIG,
  } from "@azure/msal-angular";
import { CoreModule } from "../core/core.module";

import { MSALGuardConfigFactory, MSALInstanceFactory, MSALInterceptorConfigFactory } from "./msal";
import { MicrosoftGraphSettings} from "./models/microsoftGraphSettings";
import { AzureSettings } from "./models/azureSettings";

@NgModule({
    
    imports: [CoreModule],
    providers:[]
})
export class MsalAuthModule{
    constructor(@Optional() @SkipSelf() parentModule?: MsalAuthModule){
        if (parentModule) {
            throw new Error(
                'MsalAuthModule is already loaded. Import it in the AppModule only');
        }
    }
    
    static forRoot(azureSettings:AzureSettings, microsoftGraph: MicrosoftGraphSettings, apiUrl:string):ModuleWithProviders<MsalAuthModule> {
        return{
            ngModule: MsalAuthModule,
            providers:[
                {provide:MSAL_INSTANCE, useFactory:MSALInstanceFactory, deps:[azureSettings, microsoftGraph]},
                {provide:MSAL_GUARD_CONFIG, useFactory: MSALGuardConfigFactory, deps:[azureSettings]},
                {provide: HTTP_INTERCEPTORS, useClass: MsalInterceptor, multi: true},
                {provide: MSAL_INTERCEPTOR_CONFIG, useFactory: MSALInterceptorConfigFactory, deps:[azureSettings, microsoftGraph, apiUrl]},
                MsalService,
                MsalGuard,
                MsalBroadcastService
            ]
        };
    }
}