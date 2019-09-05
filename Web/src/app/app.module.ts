import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from "@angular/router";
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LayoutsModule } from './components/common/layouts/layouts.module';
import { LoginModule } from './views/login/login.module';
import { AppInitService } from './appConfigAndBootstrap/app-init.service';
import { init } from './appConfigAndBootstrap/app.load';
import { ApiUrlsService } from './core/services/api-urls.service';
import { EnvironmentService } from './core/services/environment.service';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { RoundsModule } from './views/rounds/rounds.module';
import { ActiveRoundModule } from './views/active-round/active-round.module';
import { UsersModule } from './views/users/users.module';
import { ArchiveModule } from './views/archive/archive.module';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    RoundsModule,
    ActiveRoundModule,
    ArchiveModule,
    UsersModule,
    CoreModule,
    HttpClientModule,
    BrowserModule,
    HttpModule,
    BrowserModule,
    SharedModule,
    LoginModule,
    LayoutsModule,
    RouterModule.forRoot([])
  ],
  providers: [
    AppInitService,
    {
      provide: APP_INITIALIZER,
      useFactory: init,
      deps: [AppInitService],
      multi: true
    },
    ApiUrlsService,
    EnvironmentService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
