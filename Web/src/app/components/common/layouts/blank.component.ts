import { Component } from '@angular/core';

declare var jQuery:any;

@Component({
    selector: 'blank',
    templateUrl: 'blank.component.html'
})
export class BlankComponent {

    ngAfterViewInit() {
        jQuery('body').addClass('gray-bg');
    }

    ngOnDestroy() {
        jQuery('body').removeClass('gray-bg');
    }
}