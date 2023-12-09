<<<<<<< HEAD
import { NgModule } from '@angular/core';
=======
import { ErrorHandler, NgModule } from '@angular/core';
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
<<<<<<< HEAD
import { AccountComponent } from './features/account/account.component';
import { LoginRegisterComponent } from './features/account/login-register/login-register.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
=======
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { ErrorHandlerInterceptor } from './interceptors/error-handler.interceptor';
import { NgxSpinnerModule } from "ngx-spinner";
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
<<<<<<< HEAD
    HttpClientModule
  ],
  providers: [],
=======
    HttpClientModule,
    BrowserAnimationsModule,
    CoreModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    SharedModule,
    NgxSpinnerModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorHandlerInterceptor, multi: true }
  ],
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf
  bootstrap: [AppComponent]
})
export class AppModule { }
