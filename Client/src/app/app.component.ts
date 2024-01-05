import { Component } from '@angular/core';
import { AuthResult } from './models/account/auth-result';
import { AccountService } from './services/account.service';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  currentAuth: AuthResult | null = null;
  isAuthenticating = true;


  constructor(private accountService: AccountService,
    private router: Router) { }

  ngOnInit() {
    this.loadCurrentAuth();
    this.relogin();
    this.scrollTopWhenOpeningAComponent();
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

  scrollTopWhenOpeningAComponent() {
    this.router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe(() => window.scrollTo({
        top: 0,
        left: 0,
        behavior: 'instant'
      }))
  }

}
