import { Component } from '@angular/core';
import { smoothlyMenu } from '../../../app.helpers';
import { AuthenticationService } from '../../../core/services/authentication.service';
declare var jQuery:any;

@Component({
    selector: 'topnavbar',
    templateUrl: 'topnavbar.template.html'
})
export class TopnavbarComponent {

    constructor(
        private authService: AuthenticationService
    ) {}

    toggleNavigation(): void {
        jQuery("body").toggleClass("mini-navbar");
        smoothlyMenu();
    }

    public logOut() {
        this.authService.logOut();
    }

}
