import { Component } from '@angular/core';
import { AuthResult } from 'src/app/models/account/auth-result';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-floating-button-navigation',
  templateUrl: './floating-button-navigation.component.html',
  styleUrls: ['./floating-button-navigation.component.css']
})
export class FloatingButtonNavigationComponent {

  currentAuth: AuthResult | null = null;
  activeNav: boolean = false;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.currentAuth$.subscribe(res => this.currentAuth = res)
  }

  closeNav() {
    this.activeNav = false;
  }
}
