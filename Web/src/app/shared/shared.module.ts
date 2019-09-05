import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { BsModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';
import { NotifierModule, NotifierOptions } from 'angular-notifier';

import { ConfirmPopupComponent } from '../components/confirm-popup/confirm-popup.component';
import { HasRoleDirective } from '../components/has-role/has-role.directive';

var notifierOptions: NotifierOptions = {
    position: {
        horizontal: {
            position: 'right'
        },
        vertical: {
            position: 'top'
        }
    },
    behaviour: {
        onClick: 'hide'
    }
};

@NgModule(
    {
        imports: [
            BrowserModule,
            RouterModule,
            FormsModule,
            BsModalModule,
            NotifierModule.withConfig(notifierOptions),
        ],
        declarations: [
            ConfirmPopupComponent,
            HasRoleDirective
        ],
        exports: [
            ConfirmPopupComponent,
            HasRoleDirective,
            BrowserModule,
            RouterModule,
            FormsModule,
            BsModalModule,
            NotifierModule,
        ]
    }
)
export class SharedModule {}