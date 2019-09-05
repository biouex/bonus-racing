import { Injectable, Inject } from '@angular/core';
import { Observable, Observer, Subscription} from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { Environment } from '../models/environment.model';
import { ApiUrlsService } from './api-urls.service';
import { ApiUrls } from '../models/api-urls.model';

@Injectable()
export class EnvironmentService {
    private _environment: Environment;
    private apiUrls: ApiUrls;

    constructor(
        private http: HttpClient,
        apiUrlsService: ApiUrlsService
    ) {
        this.apiUrls = apiUrlsService.apiUrls;
    }

    get environment(): Environment {
        return this._environment;
    }

    public getEnvironment(): Observable<Environment> {
        if (!this._environment) {
            let observableEnv = Observable.create((observer: Observer<Environment>) => {
                let subscription: Subscription = this.http.get<Environment>(this.apiUrls.environment).subscribe((env) => {
                    this._environment = env;
                    observer.next(env);
                    observer.complete();
                });
                return () => {
                    subscription.unsubscribe();
                };
            });
            return observableEnv;
        }
        return Observable.of(this._environment);
    }

    public clear(): void {
        this._environment = null;
    }
}