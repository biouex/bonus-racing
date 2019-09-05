import {Injectable, Injector} from '@angular/core';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest} from '@angular/common/http';
import {AuthenticationService} from '../services/authentication.service';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private injector: Injector;

    constructor(inj: Injector) {
        this.injector = inj;
    }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const auth = this.injector.get(AuthenticationService);
    const authHeader = `Bearer ${auth.getToken()}`;
    let headers = req.headers.set('Authorization', authHeader);
    const authReq = req.clone({headers});
    return next.handle(authReq);
  }
}
