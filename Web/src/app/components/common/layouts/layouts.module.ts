import {NgModule} from "@angular/core";
import {RouterModule} from "@angular/router";
import {BrowserModule} from "@angular/platform-browser";

import {BlankComponent} from "./blank.component";
import {BasicComponent} from "./basic.component";

import {NavigationModule} from "../navigation/navigation.module";
import {TopnavbarModule} from "../topnavbar/topnavbar.module";
import {FooterModule} from "../footer/footer.module";
import { SharedModule } from "../../../shared/shared.module";

@NgModule({
    declarations: [BlankComponent,BasicComponent],
    imports     : [BrowserModule, RouterModule, NavigationModule, TopnavbarModule, FooterModule, SharedModule],
    exports     : [BlankComponent,BasicComponent]
})

export class LayoutsModule {}
