import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { take } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { emailDuplicated } from 'src/app/validators/email-duplication.validator';
import { password } from 'src/app/validators/password.validator';

@Component({
  selector: 'app-login-register',
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})
export class LoginRegisterComponent {

  loginForm = this.fb.group({
    email: ['', [Validators.required]],
    password: ['', [Validators.required]]
  });

  registerForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(20)]],
    lastName: ['', [Validators.required, Validators.maxLength(20)]],
    email: ['', [Validators.required, Validators.email], [emailDuplicated(this.accountService)]],
    password: ['', [Validators.required, password()]]
  });

  constructor(private accountService: AccountService,
    private fb: FormBuilder) { }

  isLogin = true;

  signInView() {
    this.isLogin = true;
  }

  signUpView() {
    this.isLogin = false;
  }

  signIn() {
    this.accountService.login(this.loginForm.value).pipe(take(1)).subscribe({
      next: result => console.log(result)
    });
  }

  signUp() {
    this.accountService.register(this.registerForm.value).pipe(take(1)).subscribe({
      next: result => console.log('signedUp')
    })
  }


}
