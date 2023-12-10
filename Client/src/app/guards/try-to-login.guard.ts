import { inject } from '@angular/core';
import { CanActivateFn, CanMatchFn } from '@angular/router';
import { AccountService } from '../services/account.service';
import { catchError, map, of, take } from 'rxjs';
import { AuthResult } from '../models/auth-result';

export const tryToLoginGuard: CanMatchFn = (route, state) => {
  let accountService = inject(AccountService);

  let auth: AuthResult | null = null;
  accountService.currentAuth$.pipe(take(1)).subscribe(res => auth = res);

  if (auth)
    return true;

  return accountService.relogin().pipe(map(res => res ? true : true), catchError(() => of(true)));
};
