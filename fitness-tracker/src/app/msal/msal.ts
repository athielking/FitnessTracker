import { MsalInterceptorConfiguration, MsalGuardConfiguration} from "@azure/msal-angular";
import {
  BrowserCacheLocation,
  InteractionType,
  IPublicClientApplication,
  PublicClientApplication,
  LogLevel
} from "@azure/msal-browser";

import { AzureSettings } from "./models/azure-settings";
import { MicrosoftGraphSettings } from "./models/microsoftgraph-settings";

export function loggerCallback(logLevel: LogLevel, message: string) {
    console.log(message);
}

export function MSALInstanceFactory(azureSettings:AzureSettings): IPublicClientApplication {
    return new PublicClientApplication({
      auth: {
        clientId: azureSettings.clientId,
        authority: `${azureSettings.baseAddress}${azureSettings.tenantId}`,
      },
      cache: {
        cacheLocation: BrowserCacheLocation.LocalStorage,
      },
      system: {
        loggerOptions: {
          loggerCallback,
          logLevel: LogLevel.Info,
          piiLoggingEnabled: false
        }
      }
    });
  }

  export function MSALInterceptorConfigFactory(azureSettings: AzureSettings, microsoftGraphSettings: MicrosoftGraphSettings, apiUrl:string): MsalInterceptorConfiguration {
    const protectedResourceMap = new Map<string, Array<string>>();
  
    // Define which permissions (=scopes) we need for Microsoft Graph
    protectedResourceMap.set(microsoftGraphSettings.baseAddress, microsoftGraphSettings.scopes);

    var scopes = azureSettings.aipScopes.map( (value) => {
        return `${azureSettings.api}/${value}`
    });
    protectedResourceMap.set(apiUrl, scopes);
  
    return {
      interactionType: InteractionType.Redirect,
      protectedResourceMap,
    };
  }

  /**
 * Set your default interaction type for MSALGuard here. If you have any
 * additional scopes you want the user to consent upon login, add them here as well.
 */
 export function MSALGuardConfigFactory(azureSettings: AzureSettings): MsalGuardConfiguration {
    return { 
      interactionType: InteractionType.Redirect,
      authRequest: {
        scopes:[`${azureSettings.clientId}/.default`]
      }
    };
  }