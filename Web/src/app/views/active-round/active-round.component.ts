import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Router } from '@angular/router';

import { RoundsService } from '../../core/services/rounds.service';
import { GameRound, RatingItem } from '../../core/models/rounds.model';
import { ConfirmPopupComponent } from '../../components/confirm-popup/confirm-popup.component';
import { AuthenticationService } from '../../core/services/authentication.service';

@Component({
    selector: 'active-round',
    templateUrl: 'active-round.component.html'
})
export class ActiveRoundComponent implements OnInit, OnDestroy {
    @ViewChild(ConfirmPopupComponent) confirmPopup: ConfirmPopupComponent;
    public round: GameRound;
    public rating: RatingItem[];
    public roundEnded: boolean;

    private getRatingIntervalId: number;
    private errorCounter = 0;


    constructor(
        private roundsService: RoundsService,
        private authService: AuthenticationService,
        private notifier: NotifierService
    ) {

    }

    ngOnInit(): void {
        this.loadRound();
    }

    ngOnDestroy() {
        this.stopGetRating();
    }

    public onClickSetDraft() {
        this.roundsService.setDraft().subscribe(() => {
            this.loadRound();
        });
    }

    public onClickStart() {
        this.confirmPopup.openPopup('Запустить раунд?', `Подтвердите, что хотите запустить раунд на ${this.round.duration} минут.`).subscribe(result => {
            if (result) {
                this.roundsService.start()
                    .subscribe(() => {
                        this.loadRound();
                    });
            }
        });
    }

    public onClickPause() {
        this.roundsService.pause()
            .subscribe(() => {
                this.loadRound();
            });
    }

    public onClickContinue() {
        this.roundsService.continue()
            .subscribe(() => {
                this.loadRound();
            });
    }

    public onClickOpenDemo() {
        this.authService.getTokenDemoView().subscribe(token => {
            window.open('/demonstration-view/index.html?token=' + token);
        });
    }

    public onClickComplete() {
        this.confirmPopup.openPopup('Завершение раунда', 'Вы действительно хотите завершить раунд?').subscribe((confirm) => {
            if (confirm) {
                this.roundsService.complete().subscribe(() => {
                    this.loadRound();
                });
            }
        });
    }

    private loadRound() {
        this.roundsService.getActive().subscribe(round => {
            this.round = round;
            if (round == null) {
                this.stopGetRating();
                return;
            }
            this.roundEnded = this.isEnded(round);
            if (round.state === 'active') {
                if ((round.pause == null) && (!this.isEnded(round) && (round.start != null))) {
                    this.startGetRating();
                } else {
                    this.loadRating();
                }
            } else if (round.state === 'archived')  {
                this.loadRating();
            } else {
                this.stopGetRating();
            }
        });
    }

    private loadRating() {
        if (this.round.end) {
            if (new Date(this.round.end) < new Date()) {
                this.stopGetRating();
            }
        }

        this.roundsService.getRating().subscribe(rating => {
            this.rating = rating;
        }, (err) => {
            this.errorCounter++;
            if (this.errorCounter > 10) {
                this.stopGetRating();
            }
        });
    }

    private startGetRating() {
        if (this.getRatingIntervalId != null) {
            return;
        }
        this.loadRating();
        this.getRatingIntervalId = window.setInterval(() => {
            this.loadRating();
        }, 2000);
    }

    private stopGetRating() {
        window.clearInterval(this.getRatingIntervalId);
        this.getRatingIntervalId = null;
    }

    private isEnded(round: GameRound): boolean {
        if (round.end == null) {
            return false;
        }
        var end = new Date(round.end).valueOf(),
            now = new Date().valueOf();
        return (now >= end);
    }

}