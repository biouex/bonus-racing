import { Component } from '@angular/core';
import { AuthorizeModel } from '../../core/models/authentication.model';
import { AuthenticationService } from '../../core/services/authentication.service';
import { NotifierService } from 'angular-notifier';
import { Router } from '@angular/router';
import { EnvironmentService } from '../../core/services/environment.service';

@Component({
    selector: 'login',
    templateUrl: 'login.component.html'
})
export class LoginComponent {

    constructor(
        private authService: AuthenticationService,
        private envService: EnvironmentService,
        private router: Router,
        private notifier: NotifierService
    ) { }

    public authModel: AuthorizeModel = {
        login: '',
        password: ''
    }

    public onSubmit() {
        var model: AuthorizeModel = {
            login: window.btoa(this.authModel.login),
            password: window.btoa(this.authModel.password)
        }
        this.authService.authorize(model).subscribe(res => {
            this.envService.clear();
            this.envService.getEnvironment().subscribe(env => {
                this.router.navigate(['/']);
            });
        }, (err) => {
            this.notifier.notify('error', "Wrong!")
        });
    }
}