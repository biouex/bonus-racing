import { Component, OnInit, ViewChild } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Router } from '@angular/router';

import { UsersService } from '../../core/services/users.service';
import { User } from '../../core/models/users.model';
import { ConfirmPopupComponent } from '../../components/confirm-popup/confirm-popup.component';

@Component({
    selector: 'users',
    templateUrl: 'users.component.html'
})
export class UsersComponent implements OnInit {

    @ViewChild(ConfirmPopupComponent) confirmPopup: ConfirmPopupComponent;

    public users: User[] = [];
    public page = 0;
    public count = 30;

    constructor(
        private usersService: UsersService,
        private notifier: NotifierService
    ) {

    }

    ngOnInit(): void {
        this.loadUsers();
    }

    public onClickDelete(user: User) {
        this.confirmPopup.openPopup('Удалить пользователя', `Подтвердите, что хотите удалить пользователя "${user.userName}"`).subscribe(confirm => {
            if (confirm) {
                this.usersService.delete(user.id).subscribe(() => {
                    this.notifier.notify('success', 'Пользователь удален');
                    this.users = this.users.filter(u => u != user);
                });
            }
        });
    }

    private loadUsers() {
        this.usersService.getList().subscribe(users => {
            this.users = users;
        });
    }
}