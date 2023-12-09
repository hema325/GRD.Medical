import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';

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
    private toastr: ToastrService) { }

  login() {
    this.accountService.login(this.loginForm.value).pipe(take(1)).subscribe({
      next: res => {
        let returnUrl = this.activatedRoute.snapshot.queryParamMap.get('returnUrl');
        if (!returnUrl)
          returnUrl = '/home';
        this.router.navigateByUrl(returnUrl);
        this.toastr.success('Loggedin successfully');
      },
      error: error => this.toastr.error(error.message)
    });
  }
}
