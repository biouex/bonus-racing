import {Injectable, Injector} from '@angular/core';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';

import {AuthenticationService} from '../services/authentication.service';
import { NotifierService } from 'angular-notifier';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    private injector: Injector;
    private notifier: NotifierService;

    constructor(inj: Injector) {
        this.injector = inj;
        this.notifier = this.injector.get(NotifierService);
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).do((event: HttpEvent<any>) => {
        }, (err: any) => {
            if (err instanceof HttpErrorResponse) {
                const auth = this.injector.get(AuthenticationService);

                if (err.status === 401) {
                    auth.handleNotAuthorized(err);
                } else if (err.status === 403) {
                    auth.handlePermissionDenied(err);
                } else if (err.status === 500) {
                    this.notifier.notify('error', `Error(${err.status}): ${err.error.exceptionMessage || err.message}`);
                }
            }
        });
    }
}
