import {NgModule} from "@angular/core";

import { SharedModule } from "../../shared/shared.module";
import { UsersComponent } from "./users.component";
import { UsersRoutingModule } from "./users-routing.module";
import { UserEditComponent } from "./user-edit/user-edit.component";
import { ChangePasswordPopupComponent } from "./user-edit/change-password-popup/change-password-popup.component";


@NgModule({
    declarations: [
        UsersComponent,
        UserEditComponent,
        ChangePasswordPopupComponent
    ],
    imports     : [
        SharedModule,
        UsersRoutingModule
    ],
})

export class UsersModule {}