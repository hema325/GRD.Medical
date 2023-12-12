import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http'
import { BehaviorSubject, Observable, catchError, map, of, throwError } from 'rxjs';
import { AuthResult } from '../models/auth-result';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.baseUrl + '/account';

  private currentAuth = new BehaviorSubject<AuthResult | null>(null);
  currentAuth$ = this.currentAuth.asObservable();

  constructor(private httpClient: HttpClient) { }

  login(credentials: object) {
    return this.httpClient.post<AuthResult>(this.baseUrl + '/authenticate', credentials).pipe(map((authResult): AuthResult => {
      this.currentAuth.next(authResult);
      localStorage.setItem('rftkn', JSON.stringify({ refreshToken: authResult.refreshToken, expiration: authResult.refreshTokenExpiresOn }));
      return authResult;
    }));
  }

  relogin(): Observable<AuthResult | null> {
    let tokenStr = localStorage.getItem('rftkn');
    if (tokenStr) {
      let token = JSON.parse(tokenStr);
      if (new Date().getTime() < new Date(token.expiration).getTime()) {
        return this.httpClient.post<AuthResult>(this.baseUrl + '/requestJwt', { refreshToken: token.refreshToken }).pipe(map(authResult => {
          this.currentAuth.next(authResult);
          localStorage.setItem('rftkn', JSON.stringify({ refreshToken: authResult.refreshToken, expiration: authResult.refreshTokenExpiresOn }));
          return authResult;
        }),
          catchError(err => {
            localStorage.removeItem('rftkn');
            return throwError(() => err);
          }));
      }
    }
    return of(null);
  }

  logout(): Observable<null> {

    let rftkn = this.currentAuth.value?.refreshToken;


    if (rftkn)
      return this.httpClient.post<null>(this.baseUrl + '/revokeRefreshToken', { refreshToken: rftkn }).pipe(map(res => {
        localStorage.removeItem('rftkn');
        this.currentAuth.next(null);
        return res;
      }));

    localStorage.removeItem('rftkn');
    this.currentAuth.next(null);

    return of(null);

  }

  register(registeration: object) {
    return this.httpClient.post(this.baseUrl + '/register', registeration);
  }

  isEmailDuplicated(email: string) {
    return this.httpClient.get(this.baseUrl + '/isEmailDuplicated?email=' + email);
  }

  getDetails() {
    return this.httpClient.get<User>(this.baseUrl);
  }

  update(data: any) {
    return this.httpClient.put(this.baseUrl, data).pipe(map(res => {
      let auth = this.currentAuth.value;
      if (auth) {
        auth.name = data.firstName + ' ' + data.lastName;
        this.currentAuth.next(auth);
      }
      return res;
    }));
  }

  changePassword(data: any) {
    return this.httpClient.post(this.baseUrl + '/changePassword', data);
  }

  sendEmailConfirmation(data: object) {
    return this.httpClient.post(this.baseUrl + '/sendEmailConfirmation', data);
  }

  confrimEmail(data: any) {
    return this.httpClient.post(this.baseUrl + '/confirmEmail', data);
  }

  sendEmailResetPassword(data: any) {
    return this.httpClient.post(this.baseUrl + '/sendEmailResetPassword', data);
  }


  resetPassword(data: any) {
    return this.httpClient.post(this.baseUrl + '/resetPassword', data);
  }

  uploadImage(data: any) {
    return this.httpClient.post<any>(this.baseUrl + '/uploadImage', data).pipe(map(res => {
      let auth = this.currentAuth.value;
      if (auth) {
        auth.imageUrl = res.imageUrl;
        this.currentAuth.next(auth);
      }
      return res;
    }));
  }

  removeImage() {
    return this.httpClient.post(this.baseUrl + '/removeImage', null).pipe(map(res => {
      let auth = this.currentAuth.value;
      if (auth) {
        auth.imageUrl = null;
        this.currentAuth.next(auth);
      }
    }));
  }

}

