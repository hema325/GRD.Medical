import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { LoaderService } from 'src/app/services/loader.service';
import { emailDuplicated } from 'src/app/validators/email-duplication.validator';
import { password } from 'src/app/validators/password.validator';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  registerForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(20)]],
    lastName: ['', [Validators.required, Validators.maxLength(20)]],
    email: ['', [Validators.required, Validators.email], [emailDuplicated(this.accountService, this.loader)]],
    password: ['', [Validators.required, password()]]
  });

  constructor(private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private loader: LoaderService) { }

  register() {
    this.accountService.register(this.registerForm.value).pipe(take(1)).subscribe(res => {
      this.router.navigateByUrl("/home");
      this.toastr.success('Account is registered successfully, please check your email for confirmation.');
    });
  }
}


