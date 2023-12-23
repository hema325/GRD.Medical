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

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  isRelogin = false;

  constructor(private accountService: AccountService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    let currentAuth: AuthResult | null = null;
    this.accountService.currentAuth$.pipe(take(1)).subscribe(auth => currentAuth = auth);

    if (currentAuth == null)
      return next.handle(request);

    return next.handle(this.addTokenHeader((currentAuth as AuthResult).jwtToken, request)).pipe(
      this.isRelogin ? identity : catchError(error => {
        this.isRelogin = true;
        //try to relogin if jwt expired
        if (error.statusCode == 401)
          return this.accountService.relogin().pipe(
            switchMap(auth => next.handle(this.addTokenHeader(auth?.jwtToken, request))),
            catchError(error => {
              //logout if relogin failed
              this.accountService.logout().subscribe();
              return throwError(() => error);
            }));

        return throwError(() => error);
      }),
      finalize(() => this.isRelogin = false));
  }

  addTokenHeader(token: string | undefined, request: HttpRequest<unknown>) {
    return request.clone({
      setHeaders: {
        authorization: 'Bearer ' + token
      }
    });
  }

}
