import { Component, Input, Output, EventEmitter, ViewChild, Inject } from '@angular/core';
import { BsModalComponent } from 'ng2-bs3-modal';
import { Observer, Observable } from 'rxjs';


@Component({
    selector: 'confirm-popup',
    templateUrl: 'confirm-popup.component.html',
})
export class ConfirmPopupComponent {
    @ViewChild('confirmPopup') modal: BsModalComponent;

    public header: string;
    public body: string;
    private observer: Observer<boolean>;

    constructor() {
    }

    public openPopup(title: string, message: string): Observable<boolean> {
        this.header = title;
        this.body = message;
        this.modal.open();
        return new Observable(observer => {
            this.observer = observer;
        });
    }

    public ok() {
        this.observer.next(true);
        this.observer.complete();
        this.modal.close();
    }


    public dismissPopup() {
        this.observer.next(false);
        this.observer.complete();
        this.modal.dismiss()
    }
}