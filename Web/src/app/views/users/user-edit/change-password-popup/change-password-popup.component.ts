import { Component, Input, Output, EventEmitter, ViewChild, Inject } from '@angular/core';
import { BsModalComponent } from 'ng2-bs3-modal';
import { Observer, Observable } from 'rxjs';

import { NotifierService } from 'angular-notifier';


@Component({
    selector: 'change-password-popup',
    templateUrl: 'change-password-popup.component.html',
})
export class ChangePasswordPopupComponent {
    @ViewChild('modalPopup') modal: BsModalComponent;

    public password: string;
    public passwordCopy: string;
    private observer: Observer<string>;

    constructor(
        private notifier: NotifierService
    ) { }

    public openPopup(): Observable<string> {
        this.modal.open();
        return new Observable(observer => {
            this.observer = observer;
        });
    }

    public ok() {
        if (!this.chechValidPassword()) {
            this.notifier.notify('error', 'Пароли не совпадают');
            return;
        }
        this.observer.next(this.password);
        this.observer.complete();
        this.modal.close();
    }


    public dismissPopup() {
        this.observer.next(null);
        this.observer.complete();
        this.modal.dismiss()
    }

    private chechValidPassword(): boolean {
        if (this.password == null ||
            this.password.trim().length === 0 ||
            this.passwordCopy == null || 
            this.passwordCopy.trim().length === 0 ||
            this.password !== this.passwordCopy) {
                return false;
            }
        return true;
    }
}