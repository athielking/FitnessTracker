import { CommonModule } from "@angular/common";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { RouterModule } from '@angular/router';
import { AuthInterceptor } from "../auth/auth.interceptor";
import { NavBarComponent } from "./nav-bar/nav-bar.component";
import { AppSettingsService } from "./services/appsettingsService";
import { HttpClientService } from "./services/httpclient.service";

@NgModule({   
    declarations:[NavBarComponent],
    exports:[NavBarComponent],
    imports:[
        CommonModule, RouterModule
    ],
    providers:[
        AppSettingsService,
        HttpClientService,
        {
          provide: HTTP_INTERCEPTORS,
          useClass: AuthInterceptor,
          multi: true
        }
    ]
})
export class CoreModule{}