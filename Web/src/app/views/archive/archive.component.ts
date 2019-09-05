import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Router } from '@angular/router';
import { ArchivalRoundListItemModel } from '../../core/models/rounds.model';
import { ArchivalRoundsService } from '../../core/services/archivalRounds.service';


@Component({
    selector: 'archive',
    templateUrl: 'archive.component.html'
})
export class ArchiveComponent implements OnInit {

    public archivalRounds: ArchivalRoundListItemModel[];
    public page = 0;
    public count = 30;
    public countPages: number;
    public countItems: number;

    constructor(
        private archivalRoundsService: ArchivalRoundsService,
        private notifier: NotifierService
    ) { }

    ngOnInit(): void {
        this.loadArchive();
        this.loadCountRounds();
    }

    private loadArchive() {
        this.archivalRoundsService.getList(this.page, this.count).subscribe(res => {
            this.archivalRounds = res;
        });
    }

    private loadCountRounds() {
        this.archivalRoundsService.getCount().subscribe(res => {
            this.countItems = res;
            this.countPages = Math.ceil(res/this.count);
        });
    }

    public prevPage() {
        if (this.page > 0) {
            this.page--;
            this.loadArchive();
        }
    }

    public nextPage() {
        if ((this.page+1) < this.countPages) {
            this.page++;
            this.loadArchive();
        }
    }
}