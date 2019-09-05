import { Injectable, Inject } from '@angular/core';
import { ApiUrls } from '../models/api-urls.model';
import { EnvironmentService } from './environment.service';


@Injectable()
export class ApiUrlsService {

    private baseUrl = '/api/';

    get apiUrls(): ApiUrls {
        return {
            environment: this.baseUrl + 'environment/',
            authorization: this.baseUrl + 'authorization/',
            rounds: this.baseUrl + 'rounds/',
            users: this.baseUrl + 'users/',
            archive: this.baseUrl + 'archive/'
        }
    }
}