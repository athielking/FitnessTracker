import { InjectionToken } from "@angular/core";
import { AppConfiguration } from "read-appsettings-json";
import { AzureSettings } from "src/app/msal/models/azure-settings";
import { MicrosoftGraphSettings } from "src/app/msal/models/microsoftgraph-settings";

export const AZURE_SETTINGS = new InjectionToken<AzureSettings>("AZURE_SETTINGS", {
    factory(){
        return AppConfiguration.Setting().azureSettings;
    }
});

export const MICROSOFT_GRAPH_SETTINGS = new InjectionToken<MicrosoftGraphSettings>("MICROSOFT_GRAPH_SETTINGS", {
    factory(){
        return AppConfiguration.Setting().microsoftGraph;
    }
});

export const APP_SERVER = new InjectionToken<string>("APP_SERVER", {
    factory(){
        return AppConfiguration.Setting().appServer;
    }
});