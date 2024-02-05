import { Component } from '@angular/core';
import { AccountService } from './services/account.service';
import { finalize } from 'rxjs';
import { AuthResult } from './models/account/auth-result';
import { AppInitialiserService } from './services/app-initialiser.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isAuthenticating = true;
  currentAuth: AuthResult | null = null;

  constructor(private accountService: AccountService,
    private appInitialiser: AppInitialiserService) { }

  ngOnInit() {
    this.relogin();
    this.loadCurrentAuth();
    this.appInitialiser.initialise();
  }


  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth)
  }

  relogin() {
    this.accountService.relogin()
      .pipe(finalize(() => this.isAuthenticating = false))
      .subscribe();
  }

}
