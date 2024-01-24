import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { SendEmailConfirmationComponent } from '../send-email-confirmation/send-email-confirmation.component';
import { SendEmailResetPasswordComponent } from '../send-email-reset-password/send-email-reset-password.component';
import { ErrorResponse } from 'src/app/models/Errors/error-response';
import { ErrorCodes } from 'src/app/models/Errors/error-codes.enum';

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
    private activatedRoute: ActivatedRoute) { }

  login() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => this.handleSuccessfullLogin(),
      error: error => this.handleFailureLogin(error)
    });
  }


  handleSuccessfullLogin() {
    let returnUrl = this.activatedRoute.snapshot.queryParamMap.get('returnUrl');
    if (!returnUrl)
      returnUrl = '/home';
    this.router.navigateByUrl(returnUrl);
  }

  handleFailureLogin(error: ErrorResponse) {
    if (error.errorCode == ErrorCodes.UnverifiedEmail) {
      this.router.navigate(['/account/send-email-verification'], { state: this.loginForm.controls.email.value as String });
    }
  }

}
