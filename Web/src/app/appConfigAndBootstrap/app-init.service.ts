import { Injectable, Injector } from "@angular/core";
import { Router } from "@angular/router";
import { switchMap, delay, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/observable/empty';

import { EnvironmentService } from "../core/services/environment.service";
import { ApiUrlsService } from "../core/services/api-urls.service";
import { AuthenticationService } from "../core/services/authentication.service";
import { getQueryParameter } from "../app.helpers";




@Injectable()
export class AppInitService {
    constructor(
        private environmentService: EnvironmentService,
        private authenticationService: AuthenticationService,
        private injector: Injector
    ) {}

    public prepareForBootstrap() : Promise<any> {
        let currentToken: string = this.authenticationService.getToken();
        if (!currentToken) {
            const router = this.injector.get(Router);
            router.navigate(['/login'])
        } else {
            return this.environmentService.getEnvironment().toPromise();
        }
        
    }
}