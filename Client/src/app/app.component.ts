import { Component } from '@angular/core';
import { AuthResult } from './models/account/auth-result';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  currentAuth: AuthResult | null = null;
  isAuthenticating = true;


  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.loadCurrentAuth();
    this.relogin();
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(res => this.currentAuth = res);
  }

  relogin() {
    this.accountService.relogin().subscribe({
      next: res => this.isAuthenticating = false,
      error: err => this.isAuthenticating = false
    });
  }

}
