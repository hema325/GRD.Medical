import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, catchError, concatMap, finalize, identity, of, skipUntil, switchMap, take, throwError } from 'rxjs';
import { AccountService } from '../services/account.service';
import { AuthResult } from '../models/account/auth-result';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  // isRelogin = false;
  currentAuth: AuthResult | null = null;

  constructor(private accountService: AccountService,
    private router: Router) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    this.accountService.currentAuth$.pipe(take(1)).subscribe(auth => this.currentAuth = auth);

    // if (!this.isRelogin && this.currentAuth && new Date(this.currentAuth.jwtTokenExpiresOn) < new Date()) {
    //   this.isRelogin = true;
    //   return this.accountService.relogin().pipe(switchMap(auth => {
    //     return next.handle(this.addTokenHeader(auth?.jwtToken, request));
    //   }),
    //     catchError(error => {
    //       //logout if relogin failed
    //       this.accountService.logout().subscribe();
    //       this.router.navigateByUrl('/home');
    //       return throwError(() => error);
    //     }),
    //     finalize(() => this.isRelogin = false))
    // }

    return next.handle(this.addTokenHeader(this.currentAuth?.jwtToken, request));
  }

  addTokenHeader(token: string | undefined, request: HttpRequest<unknown>) {
    return request.clone({
      setHeaders: {
        authorization: 'Bearer ' + token
      }
    });
  }

}
