import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Router, ActivatedRoute } from '@angular/router';
import { GameRound, RoundChangePlayerModel } from '../../../core/models/rounds.model';
import { Subscription } from 'rxjs';
import { RoundsService } from '../../../core/services/rounds.service';
import { Player } from '../../../core/models/players.model';
import { HttpErrorResponse } from '@angular/common/http';
import { ConfirmPopupComponent } from '../../../components/confirm-popup/confirm-popup.component';

@Component({
    selector: 'round-edit',
    templateUrl: 'round-edit.component.html'
})
export class RoundEditComponent implements OnInit, OnDestroy {
    @ViewChild(ConfirmPopupComponent) confirmPopup: ConfirmPopupComponent;

    public name: string;
    public duration: number;
    public round: GameRound;
    public newPlayerId: string;

    private roundId: string;
    private routeParamsSubscription : Subscription;
    private intervalUpdateId: number;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private roundsService: RoundsService,
        private notifier: NotifierService
    ) { }

    ngOnInit(): void {
        this.routeParamsSubscription = this.route.params.subscribe(params => {
            this.roundId = params['id'];
            this.roundsService.get(this.roundId).subscribe(res => {
                this.round = res;
                this.name = this.round.name;
                this.duration = this.round.duration;
            });
        });
        this.intervalUpdateId = window.setInterval(() => {
            this.loadPlayerList();
        }, 2000);
    }

    public ngOnDestroy(): void {
        this.routeParamsSubscription.unsubscribe();
        window.clearInterval(this.intervalUpdateId);
    }

    public onClickUpdate() {
        this.roundsService.update({
            id: this.roundId,
            name: this.name,
            duration: this.duration
        }).subscribe(() => {
            this.round.name = this.name;
            this.round.duration = this.duration;
            this.notifier.notify('success', 'Изменения сохранены!');
        });
    }

    public onClickAddPlayer() {
        if (!this.newPlayerId) {
            return;
        }

        let change: RoundChangePlayerModel = {
            id: this.roundId,
            player: {
                playerId: this.newPlayerId
            }
        }
        this.newPlayerId = null;
        this.roundsService.addPlayer(change).subscribe(res => {
            this.loadPlayerList();
        });
    }

    public onClickDeletePlayer(player: Player) {
        let change: RoundChangePlayerModel = {
            id: this.roundId,
            player: {
                playerId: player.playerId
            }
        }
        this.roundsService.deletePlayer(change).subscribe(res => {
            this.loadPlayerList();
        });
    }

    public onClickRefreshPlayerList() {
        this.loadPlayerList();
    }

    public onClickSetActive() {
        this.confirmPopup.openPopup('Назначить раунд активным', `Подтвердите, что хотите назначить раунд "${this.round.name}" активным!`)
            .subscribe(res => {
                if (res) {
                    this.roundsService.setActive(this.roundId).subscribe((result) => {
                        this.router.navigate(['/active']);
                    });
                }
            });
    }

    private loadPlayerList() {
        this.roundsService.get(this.roundId).subscribe(round => {
            this.round.players = round.players;
        });
    }
}