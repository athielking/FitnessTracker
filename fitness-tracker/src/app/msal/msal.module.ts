import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { ModuleWithProviders, NgModule, Optional, SkipSelf } from "@angular/core";

import {
    MsalBroadcastService,
    MsalGuard,
    MsalInterceptor,
    MsalService,
    MSAL_GUARD_CONFIG,
    MSAL_INSTANCE,
    MSAL_INTERCEPTOR_CONFIG,
  } from "@azure/msal-angular";

import { MSALGuardConfigFactory, MSALInstanceFactory, MSALInterceptorConfigFactory } from "./msal";
import { MsalAuthService } from "./msal.service";
import { AZURE_SETTINGS, MICROSOFT_GRAPH_SETTINGS } from "../core/services/appsettings.services";

@NgModule()
export class MsalAuthModule{
    constructor(@Optional() @SkipSelf() parentModule?: MsalAuthModule){
        if (parentModule) {
            throw new Error(
                'MsalAuthModule is already loaded. Import it in the AppModule only');
        }
    }
    
    static forRoot():ModuleWithProviders<MsalAuthModule> {
        return({
            ngModule: MsalAuthModule,
            providers:[
                {
                    provide: MSAL_INSTANCE,
                    useFactory: MSALInstanceFactory,
                    deps:[AZURE_SETTINGS]
                },
                {
                    provide: MSAL_INTERCEPTOR_CONFIG,
                    useFactory: MSALInterceptorConfigFactory,
                    deps:[MICROSOFT_GRAPH_SETTINGS]
                },
                {
                    provide: HTTP_INTERCEPTORS,
                    useClass: MsalInterceptor,
                    multi: true,
                },
                {
                    provide: MSAL_GUARD_CONFIG, 
                    useFactory: MSALGuardConfigFactory, 
                    deps:[AZURE_SETTINGS]
                },
                MsalService,
                MsalGuard,
                MsalBroadcastService,
                MsalAuthService
            ]
        });
    }
}