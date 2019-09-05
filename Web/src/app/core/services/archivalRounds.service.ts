import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ApiUrls } from '../models/api-urls.model';
import { ApiUrlsService } from './api-urls.service';
import { User, UserCreateModel, ChangePasswordModel } from '../models/users.model';
import { ArchivalRoundListItemModel, ArchivalRoundModel } from '../models/rounds.model';



@Injectable()
export class ArchivalRoundsService {

    private apiUrls: ApiUrls;

    constructor(
        private http: HttpClient,
        apiUrlsService: ApiUrlsService
    ) {
        this.apiUrls = apiUrlsService.apiUrls
    }

    public get(id: string): Observable<ArchivalRoundModel> {
        return this.http.get<ArchivalRoundModel>(this.apiUrls.archive + id);
    }

    public getList(page: number, count: number): Observable<ArchivalRoundListItemModel[]> {
        let params = new HttpParams()
            .append('page', page.toString())
            .append('count', count.toString());
        return this.http.get<ArchivalRoundListItemModel[]>(`${this.apiUrls.archive}list`, { params: params });
    }

    public getCount(): Observable<number> {
        return this.http.get<number>(`${this.apiUrls.archive}count`);
    }
}