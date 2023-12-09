import { Component } from '@angular/core';
import { AuthResult } from './models/auth-result';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  currentAuth: AuthResult | null = null;


  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

}
