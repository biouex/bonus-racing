import { Injectable, Inject, Injector } from '@angular/core'
import { HttpClient } from '@angular/common/http';
import { LocalStorageService } from '../storage/local-storage.service'
import { Observable } from 'rxjs/Observable';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

import { ApiUrlsService } from './api-urls.service';
import { ApiUrls } from '../models/api-urls.model';
import { AuthorizeModel, TokenResult } from '../models/authentication.model';


@Injectable()
export class AuthenticationService {

    private tokenStorageKey = 'authToken';
    private apiUrls: ApiUrls;

    constructor(
        private http: HttpClient,
        private storage: LocalStorageService,
        private location: Location,
        private apiUrlsService: ApiUrlsService,
        private injector: Injector
    ) {
        this.apiUrls = apiUrlsService.apiUrls;
    }

    

    public getToken(): string {
        return this.storage.read(this.tokenStorageKey);
    }

    public handleNotAuthorized(errorResponse: any): void {
        console.log('You are not authorized');
        this.logOut();
    }

    public handlePermissionDenied(errorResponse: any): void {
        console.log('You don\'t have enough permissions for the action');
    }

    public authorize(authorizeModel: AuthorizeModel): Observable<TokenResult> {
        return this.http.post<TokenResult>(`${this.apiUrls.authorization}authorize`, authorizeModel).pipe(map((data) => {
            if (data) {
                this.saveToken(data.token);
            } else {
                console.log(data);
            }
            return data;
        }, function (response) {
            console.log(response);
        }));
    }

    public getTokenDemoView(): Observable<string> {
        return this.http.get<TokenResult>(`${this.apiUrls.authorization}demoToken`).pipe(map((data) => {
            return data.token;
        }));
    }

    public logOut(): void {
        this.removeAuthToken();
        const router = this.injector.get(Router);
        //router.navigate(['/']);
        window.location.href = window.location.origin + '/login';
    }

    private removeAuthToken(): void {
        this.storage.delete(this.tokenStorageKey);
    }

    private saveToken(token: string): void {
        this.storage.save(this.tokenStorageKey, token);
    }
}
