import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { SendEmailConfirmationComponent } from '../send-email-confirmation/send-email-confirmation.component';
import { SendEmailResetPasswordComponent } from '../send-email-reset-password/send-email-reset-password.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm = this.fb.group({
    email: ['', [Validators.required]],
    password: ['', [Validators.required]]
  });


  constructor(private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private toastr: ToastrService,
    private dialog: MatDialog) { }

  login() {
    this.accountService.login(this.loginForm.value).pipe(take(1)).subscribe(res => {
      let returnUrl = this.activatedRoute.snapshot.queryParamMap.get('returnUrl');
      if (!returnUrl)
        returnUrl = '/home';
      this.router.navigateByUrl(returnUrl);
      this.toastr.success('Loggedin successfully');
    });
  }

  sendEmailConfirmation() {
    this.dialog.open(SendEmailConfirmationComponent, { width: '90%', maxWidth: '800px' });
  }

  sendEmailResetPassword() {
    this.dialog.open(SendEmailResetPasswordComponent, { width: '90%', maxWidth: '800px' });
  }
}
