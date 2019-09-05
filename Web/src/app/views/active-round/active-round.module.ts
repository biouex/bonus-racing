import {NgModule} from "@angular/core";

import { SharedModule } from "../../shared/shared.module";
import { ActiveRoundComponent } from "./active-round.component";
import { ActiveRoundRoutingModule } from "./active-round-routing.module";

@NgModule({
    declarations: [
        ActiveRoundComponent,
    ],
    imports     : [
        SharedModule,
        ActiveRoundRoutingModule
    ],
})

export class ActiveRoundModule {}