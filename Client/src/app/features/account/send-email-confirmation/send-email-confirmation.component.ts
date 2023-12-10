import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-send-email-confirmation',
  templateUrl: './send-email-confirmation.component.html',
  styleUrls: ['./send-email-confirmation.component.css']
})
export class SendEmailConfirmationComponent {


  emailForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]]
  })

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private toastr: ToastrService,
    private router: Router) { }

  send() {
    this.accountService.sendEmailConfirmation(this.emailForm.value).subscribe({
      next: res => {
        this.toastr.success('Email sent successfully');
        this.router.navigateByUrl('/home');
      },
      error: err => {
        if (err.statusCode != 500)
          this.toastr.error(err.message);
      }
    });
  }

}
