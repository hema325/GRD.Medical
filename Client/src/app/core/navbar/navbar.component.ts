import { Component, Renderer2, ViewChild } from '@angular/core';
import { AuthResult } from 'src/app/models/account/auth-result';
import { AccountService } from 'src/app/services/account.service';
import { environment } from 'src/environments/environment.development';
import { NavMenuComponent } from '../nav-menu/nav-menu.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  defaultUserImageUrl = environment.defaultUserImageUrl;
  currentAuth: AuthResult | null = null;
  isNavlinksActive = false;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    console.log()
    this.accountService.currentAuth$.subscribe(result => this.currentAuth = result);
  }

  @ViewChild('navMenu') navMenu?: NavMenuComponent;

  toggleNavMenu() {
    this.navMenu?.toggle();
  }

  deactivateNavMenu() {
    this.navMenu?.deactivate();
  }
}
