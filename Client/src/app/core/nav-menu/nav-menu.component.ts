import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  isNavMenuActive: boolean = false;

  constructor(
    public accountService: AccountService,
    private router: Router) { }

  logout() {
    this.accountService.logout().subscribe(res => this.router.navigateByUrl('/home'));
  }

  activate() {
    this.isNavMenuActive = true;
  }

  deactivate() {
    this.isNavMenuActive = false;
  }

  toggle() {
    this.isNavMenuActive = !this.isNavMenuActive;
  }

}
