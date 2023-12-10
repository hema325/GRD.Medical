import { Component } from '@angular/core';
import { AuthResult } from 'src/app/models/auth-result';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  currentAuth: AuthResult | null = null;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }
}
