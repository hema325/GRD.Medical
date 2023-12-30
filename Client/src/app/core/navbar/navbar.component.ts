import { Component, Renderer2 } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginComponent } from 'src/app/features/account/login/login.component';
import { RegisterComponent } from 'src/app/features/account/register/register.component';
import { AuthResult } from 'src/app/models/account/auth-result';
import { AccountService } from 'src/app/services/account.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  defaultUserImageUrl = environment.defaultUserImageUrl;
  currentAuth: AuthResult | null = null;
  isDropdownActive = false;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.currentAuth$.subscribe(result => this.currentAuth = result);
  }
}
