import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http'
import { Observable, ReplaySubject, catchError, map, of, throwError } from 'rxjs';
import { AuthResult } from '../models/account/auth-result';
import { User } from '../models/users/user';
import { Media } from '../models/media';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.baseUrl + '/account';

  private currentAuth = new ReplaySubject<AuthResult | null>(1);
  currentAuth$ = this.currentAuth.asObservable();

  private currentAuthResult: AuthResult | null = null;

  constructor(private httpClient: HttpClient) { }

  private setCurrentAuth(currentAuth: AuthResult) {
    this.currentAuth.next(currentAuth);
    localStorage.setItem('rftkn', JSON.stringify({ refreshToken: currentAuth.refreshToken, expiration: currentAuth.refreshTokenExpiresOn }));
    this.currentAuthResult = currentAuth;
  }

  private removeCurrentAuth() {
    localStorage.removeItem('rftkn');
    this.currentAuth.next(null);
    this.currentAuthResult = null;
  }

  login(credentials: object) {
    return this.httpClient.post<AuthResult>(this.baseUrl + '/authenticate', credentials).pipe(map((authResult): AuthResult => {
      this.setCurrentAuth(authResult);
      return authResult;
    }));
  }

  relogin(): Observable<AuthResult | null> {
    let tokenStr = localStorage.getItem('rftkn');
    if (tokenStr) {
      let token = JSON.parse(tokenStr);
      if (new Date() < new Date(token.expiration)) {
        return this.httpClient.post<AuthResult>(this.baseUrl + '/requestJwt', { refreshToken: token.refreshToken })
          .pipe(map(authResult => {
            this.setCurrentAuth(authResult);
            return authResult;
          }),
            catchError(err => {
              this.removeCurrentAuth();
              return throwError(() => err);
            }));
      }
    }
    this.currentAuth.next(null);
    return of(null);
  }

  logout(): Observable<null> {
    let rftkn = this.currentAuthResult?.refreshToken;

    if (rftkn)
      return this.httpClient.post<null>(this.baseUrl + '/revokeRefreshToken', { refreshToken: rftkn }).pipe(map(res => {
        this.removeCurrentAuth();
        return res;
      }));

    this.removeCurrentAuth();
    return of(null);
  }

  registerUser(registeration: object) {
    return this.httpClient.post(this.baseUrl + '/registerUser', registeration);
  }

  registerDoctor(registeration: object) {
    return this.httpClient.post(this.baseUrl + '/registerDoctor', registeration);
  }

  isEmailDuplicated(email: string) {
    return this.httpClient.post(this.baseUrl + '/isEmailDuplicated', { email: email });
  }

  getDetails() {
    return this.httpClient.get<User>(this.baseUrl);
  }

  updateUser(data: any) {
    return this.httpClient.put(this.baseUrl + '/user', data).pipe(map(res => {
      let auth = this.currentAuthResult;
      if (auth) {
        auth.name = data.firstName + ' ' + data.lastName;
        this.currentAuth.next(auth);
      }
      return res;
    }));
  }

  updateDoctor(data: any) {
    return this.httpClient.put(this.baseUrl + '/doctor', data).pipe(map(res => {
      let auth = this.currentAuthResult;
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
    return this.httpClient.post<AuthResult>(this.baseUrl + '/confirmEmail', data).pipe(map(res => {
      this.setCurrentAuth(res);
      return res;
    }));
  }

  sendEmailResetPassword(data: any) {
    return this.httpClient.post(this.baseUrl + '/sendEmailResetPassword', data);
  }


  resetPassword(data: any) {
    return this.httpClient.post(this.baseUrl + '/resetPassword', data);
  }

  uploadImage(data: any) {
    var fd = new FormData();
    fd.append('image', data);

    return this.httpClient.post<Media>(this.baseUrl + '/uploadImage', fd).pipe(map(res => {
      let auth = this.currentAuthResult;
      if (auth) {
        auth.imageUrl = res.url;
        this.currentAuth.next(auth);
      }
      return res;
    }));
  }

  removeImage() {
    return this.httpClient.post(this.baseUrl + '/removeImage', null).pipe(map(res => {
      let auth = this.currentAuthResult;
      if (auth) {
        auth.imageUrl = null;
        this.currentAuth.next(auth);
      }
    }));
  }

}

