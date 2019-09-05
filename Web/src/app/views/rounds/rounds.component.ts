import { Component, OnInit, ViewChild } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Router } from '@angular/router';
import { RoundsService } from '../../core/services/rounds.service';
import { GameRound } from '../../core/models/rounds.model';
import { NewRoundPopupComponent } from './new-round-popup/new-round-popup.component';

@Component({
    selector: 'rounds',
    templateUrl: 'rounds.component.html'
})
export class RoundsComponent implements OnInit {
    @ViewChild(NewRoundPopupComponent) newRoundPopup: NewRoundPopupComponent;

    public rounds: GameRound[] = [];

    constructor(
        private roundsService: RoundsService,
        private notifier: NotifierService
    ) { }

    ngOnInit(): void {
        this.loadRounds();
    }

    public onClickNewRound() {
        this.newRoundPopup.openPopup(this.rounds).subscribe(result => {
            if (result != null) {
                this.roundsService.create(result).subscribe(() => {
                    this.loadRounds();
                });
            }
        });
    }

    private loadRounds() {
        this.roundsService.getRounds().subscribe(rounds => {
            this.rounds = rounds;
        });
    }
}