import { AppConfiguration } from "read-appsettings-json";
import { AzureSettings } from "src/app/msal/models/azureSettings";
import { MicrosoftGraphSettings } from "src/app/msal/models/microsoftGraphSettings";

export function appServer():string{
    return AppConfiguration.Setting().appServer;
}
    
export function getMicrosoftGraphSettings():MicrosoftGraphSettings{
    return AppConfiguration.Setting().microsoftGraph;
}  

export function getAzureSettings():AzureSettings{
    return AppConfiguration.Setting().azureSettings;
}
