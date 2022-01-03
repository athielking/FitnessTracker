import { Injectable } from "@angular/core";
import { AppConfiguration } from "read-appsettings-json";
import { AzureSettings } from "src/app/auth/models/azureSettings";
import { MicrosoftGraph } from "src/app/auth/models/microsoftGraphSettings";

@Injectable({
    providedIn:'root'
})
export class AppSettingsService{
    
    appServer():string{
        return AppConfiguration.Setting().appServer;
    }
    
    getMicrosoftGraphSettings():MicrosoftGraph{
        return AppConfiguration.Setting().microsoftGraph;
    }

    getAzureSettings():AzureSettings{
        return AppConfiguration.Setting().azureSettings;
    }

    getAipScopes():string[]{
        return AppConfiguration.Setting().aipScopes;
    }
}