import {NgModule} from "@angular/core";

import { SharedModule } from "../../shared/shared.module";
import { RoundsComponent } from "./rounds.component";
import { RoundsRoutingModule } from "./rounds-routing.module";
import { NewRoundPopupComponent } from "./new-round-popup/new-round-popup.component";
import { RoundEditComponent } from "./round-edit/round-edit.component";

@NgModule({
    declarations: [
        RoundsComponent,
        RoundEditComponent,
        NewRoundPopupComponent
    ],
    imports     : [
        SharedModule,
        RoundsRoutingModule
    ],
})

export class RoundsModule {}