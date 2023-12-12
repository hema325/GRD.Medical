import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-send-email-reset-password',
  templateUrl: './send-email-reset-password.component.html',
  styleUrls: ['./send-email-reset-password.component.css']
})
export class SendEmailResetPasswordComponent {

  emailForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]]
  })

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService) { }


  send() {
    this.accountService.sendEmailResetPassword(this.emailForm.value).subscribe(res => {
      this.toastr.success('Email sent successfully.');
      this.router.navigateByUrl('/home');
    });
  }
}
