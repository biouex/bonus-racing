import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ApiUrls } from '../models/api-urls.model';
import { ApiUrlsService } from './api-urls.service';
import { User, UserCreateModel, ChangePasswordModel } from '../models/users.model';



@Injectable()
export class UsersService {

    private apiUrls: ApiUrls;

    constructor(
        private http: HttpClient,
        apiUrlsService: ApiUrlsService
    ) {
        this.apiUrls = apiUrlsService.apiUrls
    }

    public get(id: string): Observable<User> {
        return this.http.get<User>(this.apiUrls.users + id);
    }

    public getList(): Observable<User[]> {
        return this.http.get<User[]>(this.apiUrls.users);
    }

    public create(user: UserCreateModel): Observable<void> {
        return this.http.post<void>(this.apiUrls.users, user);
    }

    public update(model: User): Observable<void> {
        return this.http.post<void>(`${this.apiUrls.users}update`, model);
    }

    public delete(id: string): Observable<void> {
        return this.http.post<void>(this.apiUrls.users + id, {});
    }

    public changePassword(model: ChangePasswordModel) : Observable<void> {
        return this.http.post<void>(`${this.apiUrls.users}changePassword`, model);
    }
}