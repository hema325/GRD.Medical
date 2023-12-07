import { Component, Renderer2 } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginComponent } from 'src/app/features/account/login/login.component';
import { RegisterComponent } from 'src/app/features/account/register/register.component';
import { AuthResult } from 'src/app/models/auth-result';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  currentAuth: AuthResult | null = null;
  isDropdownActive = false;

  constructor(private dialog: MatDialog,
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService,
    private renderer: Renderer2) { }


  ngOnInit() {
    this.accountService.currentAuth$.subscribe(result => this.currentAuth = result);
  }

  logout() {
    this.accountService.logout().subscribe(res => {
      this.router.navigateByUrl('/home');
      this.toastr.success('logged out successfully');
    });
  }

}
