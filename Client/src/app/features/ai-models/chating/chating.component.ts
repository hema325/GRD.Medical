import { Component } from '@angular/core';
import { AuthResult } from 'src/app/models/auth-result';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-chating',
  templateUrl: './chating.component.html',
  styleUrls: ['./chating.component.css']
})
export class ChatingComponent {

  currentAuth: AuthResult | null = null;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

}
