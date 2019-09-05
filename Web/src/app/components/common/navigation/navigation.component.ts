import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { EnvironmentService } from '../../../core/services/environment.service';

declare var jQuery:any;

@Component({
    selector: 'navigation',
    templateUrl: 'navigation.template.html'
})

export class NavigationComponent {

    public userName: string;

    constructor(
        private router: Router,
        environmentService: EnvironmentService
    ) {
        environmentService.getEnvironment().subscribe(env => {
            this.userName = env.userName;
        });
    }

    ngAfterViewInit() {
        jQuery('#side-menu').metisMenu();
    }

    activeRoute(routename: string): boolean{
        return this.router.url.indexOf(routename) > -1;
    }
}