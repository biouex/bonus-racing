import { Component, OnInit } from '@angular/core';

import { ArchivalRoundsService } from '../../../core/services/archivalRounds.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ArchivalRoundModel, RatingItem } from '../../../core/models/rounds.model';
import { GameData } from '../../../core/models/gameData.model';

@Component({
    selector: 'archive-view',
    templateUrl: 'archive-view.component.html'
})
export class ArchiveViewComponent implements OnInit {

    public round: ArchivalRoundModel;
    public players: RatingItem[];
    public selectedPlayerData: GameData[];
    public selectedPlayer: string;

    private routeParamsSubscription : Subscription;
    private id: string;

    constructor(
        private route: ActivatedRoute,
        private archivalRoundsService: ArchivalRoundsService
    ) {}

    ngOnInit(): void {
        this.routeParamsSubscription = this.route.params.subscribe(params => {
            this.id = params['id'];
            this.loadRound();
        });
    }

    private loadRound() {
        this.archivalRoundsService.get(this.id).subscribe(model => {
            this.round = model;
            this.players = this.getRatingItems(this.round);
        });
    }

    private getRatingItems(round: ArchivalRoundModel): RatingItem[] {
        return round.players.map(player => {
            var points = round.gameData
                .filter(data => data.idenCardId == player.playerId.toString())
                .reduce((accum, current, index, arr) => {
                    return accum + parseFloat(current.earnedPoints);
                }, 0);
            return {
                playerId: player.playerId.toString(),
                points: parseFloat(points.toFixed(2))
            };
        }).sort((a,b) => {
            return b.points - a.points;
        });
    }

    public onSelectPlayer(playerId: string) {
        this.selectedPlayer = playerId;
        this.selectedPlayerData = this.round.gameData
            .filter(data => {
                return data.idenCardId == playerId;
            });
    }
}