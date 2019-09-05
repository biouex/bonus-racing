import {NgModule} from "@angular/core";

import { SharedModule } from "../../shared/shared.module";
import { ArchiveComponent } from "./archive.component";
import { ArchiveRoutingModule } from "./archive-routing.module";
import { ArchiveViewComponent } from "./archive-view/archive-view.component";

@NgModule({
    declarations: [
        ArchiveComponent,
        ArchiveViewComponent
    ],
    imports     : [
        SharedModule,
        ArchiveRoutingModule
    ],
})

export class ArchiveModule {}