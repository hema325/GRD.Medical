import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { password } from 'src/app/validators/password.validator';

@Component({
  selector: 'app-change-account-password',
  templateUrl: './change-account-password.component.html',
  styleUrls: ['./change-account-password.component.css']
})
export class ChangeAccountPasswordComponent {

  passwordForm = this.fb.group({
    oldPassword: ['', Validators.required],
    newPassword: ['', [Validators.required, password()]],
  })

  constructor(private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService) { }

  changePassword() {
    this.accountService.changePassword(this.passwordForm.value).subscribe({
      next: res => {
        this.router.navigateByUrl("/account/login");
        this.accountService.logout().pipe(take(1)).subscribe();
        this.toastr.success('Password changed successfully');
      }
    })
  }
}
