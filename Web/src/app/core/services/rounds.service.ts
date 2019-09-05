import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ApiUrls } from '../models/api-urls.model';
import { EnvironmentService } from './environment.service';
import { GameRound, RoundCreateModel, RoundChangePlayerModel, RatingItem, RoundUpdateModel } from '../models/rounds.model';
import { ApiUrlsService } from './api-urls.service';



@Injectable()
export class RoundsService {

    private apiUrls: ApiUrls;

    constructor(
        private http: HttpClient,
        apiUrlsService: ApiUrlsService
    ) {
        this.apiUrls = apiUrlsService.apiUrls
    }

    public get(id: string): Observable<GameRound> {
        return this.http.get<GameRound>(this.apiUrls.rounds + id);
    }

    public getRounds(): Observable<GameRound[]> {
        return this.http.get<GameRound[]>(this.apiUrls.rounds);
    }

    public getActive(): Observable<GameRound> {
        return this.http.get<GameRound>(`${this.apiUrls.rounds}active`)
    }

    public create(round: RoundCreateModel): Observable<void> {
        return this.http.post<void>(this.apiUrls.rounds, round);
    }

    public update(round: RoundUpdateModel): Observable<void> {
        return this.http.post<void>(`${this.apiUrls.rounds}update`, round);
    }

    public addPlayer(model: RoundChangePlayerModel): Observable<void> {
        return this.http.post<void>(`${this.apiUrls.rounds}addPlayer`, model);
    }

    public deletePlayer(model: RoundChangePlayerModel): Observable<void> {
        return this.http.post<void>(`${this.apiUrls.rounds}deletePlayer`, model);
    }

    public setActive(id: string): Observable<void> {
        return this.http.post<void>(`${this.apiUrls.rounds}setActive`, { id: id });
    }

    public setDraft(): Observable<void> {
        return this.http.post<void>(`${this.apiUrls.rounds}setDraft`, {});
    }

    public start(): Observable<void> {
        return this.http.post<void>(`${this.apiUrls.rounds}start`, {});
    }

    public complete(): Observable<void> {
        return this.http.post<void>(`${this.apiUrls.rounds}complete`, {});
    }

    public pause(): Observable<void> {
        return this.http.post<void>(`${this.apiUrls.rounds}pause`, {});
    }

    public continue(): Observable<void> {
        return this.http.post<void>(`${this.apiUrls.rounds}continue`, {});
    }

    public getRating(): Observable<RatingItem[]> {
        return this.http.get<RatingItem[]>(`${this.apiUrls.rounds}rating`);
    }
}