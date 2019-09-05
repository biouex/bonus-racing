import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Router, ActivatedRoute } from '@angular/router';
import { GameRound, RoundChangePlayerModel } from '../../../core/models/rounds.model';
import { Subscription } from 'rxjs';
import { RoundsService } from '../../../core/services/rounds.service';
import { Player } from '../../../core/models/players.model';
import { HttpErrorResponse } from '@angular/common/http';
import { User, UserCreateModel, RoleCheckboxModel, ChangePasswordModel } from '../../../core/models/users.model';
import { UsersService } from '../../../core/services/users.service';
import { ChangePasswordPopupComponent } from './change-password-popup/change-password-popup.component';

@Component({
    selector: 'user-edit',
    templateUrl: 'user-edit.component.html'
})
export class UserEditComponent implements OnInit, OnDestroy {
    @ViewChild(ChangePasswordPopupComponent) changePasswordPopup: ChangePasswordPopupComponent;


    public user: User;
    public userId: string;
    public password: string;

    public selectedRole;
    public roles = [
        { label: 'Оператор', ids: [ 'roundEdit' ] },
        { label: 'Администратор', ids: [ 'roundEdit', 'roundStart', 'userManagement' ]}
    ];

    private routeParamsSubscription : Subscription;

    constructor(
        private route: ActivatedRoute,
        private usersService: UsersService,
        private notifier: NotifierService,
        private router: Router
    ) {

    }

    ngOnInit(): void {
        this.routeParamsSubscription = this.route.params.subscribe(params => {
            this.userId = params['id'];
            if (this.userId === 'new') {
                this.user = {
                    firstName: '',
                    lastName: '',
                    roles: [],
                    userName: '',
                    id: ''
                };
                this.selectedRole = this.roles[0];
            } else {
                this.usersService.get(this.userId).subscribe(user => {
                    this.user = user;
                    if (user.roles && user.roles.length > 0) {
                        this.selectedRole = this.roles.find(role => {
                            return ((role.ids.length === user.roles.length) &&
                                (user.roles.every(roleId => role.ids.indexOf(roleId) >= 0)));
                        });
                    }
                    if (this.selectedRole == null) {
                        this.selectedRole = this.roles[0];
                    }
                });
            }
        });
        
    }

    public ngOnDestroy(): void {
        this.routeParamsSubscription.unsubscribe();
    }

    public onClickSave(): void {
        let roles: string[] = this.selectedRole.ids;
        

        if (this.userId === 'new') {
            let model: UserCreateModel = {
                userName: this.user.userName,
                firstName: this.user.firstName,
                lastName: this.user.lastName,
                roles: roles,
                password: this.password
            };

            this.usersService.create(model).subscribe(() => {
                this.notifier.notify('success', 'Пользователь создан');
                this.router.navigate(['/users']);
            })
        } else {
            this.user.roles = roles;
            this.usersService.update(this.user).subscribe(() => {
                this.notifier.notify('success', 'Пользователь сохранен');
                this.router.navigate(['/users']);
            });
        }
    }

    public onClickChangePassword() {
        this.changePasswordPopup.openPopup().subscribe(password => {
            if (password != null) {
                let encryptedPassword = window.btoa(password);
                this.usersService.changePassword({ id: this.userId, password: encryptedPassword}).subscribe(() => {
                    this.notifier.notify('success', 'Пароль успешно изменен');
                });
            }
        })
    }
}