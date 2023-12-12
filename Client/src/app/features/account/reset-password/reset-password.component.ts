import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/services/account.service';
import { password } from 'src/app/validators/password.validator';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {

  resetPasswordForm = this.fb.group({
    userId: ['', [Validators.required]],
    token: ['', [Validators.required]],
    password: ['', [Validators.required, password()]],
  })

  constructor(private router: Router,
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private toastr: ToastrService,
    private fb: FormBuilder) { }

  ngOnInit() {
    let userId = this.activatedRoute.snapshot.queryParamMap.get('userId');
    let token = this.activatedRoute.snapshot.queryParamMap.get('token');
    this.resetPasswordForm.patchValue({ userId: userId, token: token })
  }

  resetPassword() {
    this.accountService.resetPassword(this.resetPasswordForm.value).subscribe({
      next: res => {
        this.router.navigateByUrl('/account/login');
        this.toastr.success('Password reseted successfully.');
      },
      error: err => this.router.navigateByUrl('/home')
    });
  }

}
