import { Component, Input, Output, EventEmitter, ViewChild, Inject } from '@angular/core';
import { BsModalComponent } from 'ng2-bs3-modal';
import { Observer, Observable } from 'rxjs';

import { GameRound, RoundCreateModel } from '../../../core/models/rounds.model';


@Component({
    selector: 'new-round-popup',
    templateUrl: 'new-round-popup.component.html',
})
export class NewRoundPopupComponent {
    @ViewChild('modalPopup') modal: BsModalComponent;

    public name: string;
    public duration: number;
    public copyBy: string;
    public rounds: GameRound[];
    private observer: Observer<RoundCreateModel>;

    constructor() {
    }

    public openPopup(rounds: GameRound[]): Observable<RoundCreateModel> {
        this.name = '';
        this.duration = 60;
        this.copyBy = '';

        this.rounds = rounds;
        this.modal.open();
        return new Observable(observer => {
            this.observer = observer;
        });
    }

    public ok() {

        if (this.name == null || this.name.trim().length === 0) {
            return;
        }
        let model: RoundCreateModel = {
            name: this.name.trim(),
            duration: this.duration,
            copyBy: (this.copyBy.length > 0) ? this.copyBy : null
        };
        this.observer.next(model);
        this.observer.complete();
        this.modal.close();
    }


    public dismissPopup() {
        this.observer.next(null);
        this.observer.complete();
        this.modal.dismiss()
    }
}