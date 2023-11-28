import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http'
import { BehaviorSubject, map } from 'rxjs';
import { AuthResult } from '../models/auth-result';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.baseUrl + '/api/account';

  private currentAuth = new BehaviorSubject<AuthResult | null>(null);
  currentAuth$ = this.currentAuth.asObservable();

  constructor(private httpClient: HttpClient) { }

  login(credentials: object) {
    return this.httpClient.post<AuthResult>(this.baseUrl + '/authenticate', credentials).pipe(map((authResult): AuthResult => {
      this.currentAuth.next(authResult);
      return authResult;
    }));
  }

  logout() {
    if (this.currentAuth.value)
      this.httpClient.post(this.baseUrl + '/revokeRefreshToken', this.currentAuth.value.refreshToken);

    this.currentAuth.next(null);
  }

  register(registeration: object) {
    return this.httpClient.post(this.baseUrl + '/register', registeration);
  }

  isEmailDuplicated(email: string) {
    return this.httpClient.get(this.baseUrl + '/isEmailDuplicated?email=' + email);
  }

}
