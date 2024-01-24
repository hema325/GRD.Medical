import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { LoaderService } from 'src/app/services/loader.service';
import { emailDuplicated } from 'src/app/validators/email-duplication.validator';
import { password } from 'src/app/validators/password.validator';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent {

  registerForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(20)]],
    lastName: ['', [Validators.required, Validators.maxLength(20)]],
    email: ['', [Validators.required, Validators.email], [emailDuplicated(this.accountService, this.loader)]],
    password: ['', [Validators.required, password()]]
  });

  constructor(private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router,
    private loader: LoaderService) { }

  register() {
    this.accountService.registerUser(this.registerForm.value).pipe(take(1)).subscribe(res => {
      this.router.navigate(["/account/send-email-verification"], { state: this.registerForm.value.email as String });
    });
  }
}


