import { Component } from '@angular/core';
<<<<<<< HEAD
=======
import { AuthResult } from './models/auth-result';
import { AccountService } from './services/account.service';
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
<<<<<<< HEAD
  title = 'Client';
=======

  currentAuth: AuthResult | null = null;


  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf
}
