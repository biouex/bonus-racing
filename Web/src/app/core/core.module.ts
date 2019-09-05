import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LocalStorageService } from './storage/local-storage.service';
import { AuthInterceptor } from './interceptors/auth-interceptor';
import { ErrorInterceptor } from './interceptors/error-interceptor';
import { AuthenticationService } from './services/authentication.service';
import { RoundsService } from './services/rounds.service';
import { UsersService } from './services/users.service';
import { ArchivalRoundsService } from './services/archivalRounds.service';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    LocalStorageService,
    AuthenticationService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    LocalStorageService,
    RoundsService,
    UsersService,
    ArchivalRoundsService
  ],
  declarations: []
})
export class CoreModule { }
