import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { LoaderService } from 'src/app/services/loader.service';
import { emailDuplicated } from 'src/app/validators/email-duplication.validator';
import { password } from 'src/app/validators/password.validator';
import { precisionScale } from 'src/app/validators/precision-scale.validator';

@Component({
  selector: 'app-register-doctor',
  templateUrl: './register-doctor.component.html',
  styleUrls: ['./register-doctor.component.css']
})
export class RegisterDoctorComponent {

  registerForm = this.fb.group({
    AccountInfo: this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(20)]],
      lastName: ['', [Validators.required, Validators.maxLength(20)]],
      email: ['', [Validators.required, Validators.email], [emailDuplicated(this.accountService, this.loader)]],
      password: ['', [Validators.required, password()]]
    }),
    DoctorInfo: this.fb.group({
      experience: ['', [Validators.required, Validators.max(60)]],
      consultFee: ['', [Validators.required, precisionScale(9, 2)]],
      specialityId: ['', [Validators.required]],
      languageIds: ['', [Validators.required]],
      biography: ['', [Validators.required]]
    })
  });

  constructor(private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router,
    private loader: LoaderService) { }

  register() {
    const formObj = this.registerForm.value;
    this.accountService.registerDoctor({ ...formObj.AccountInfo, ...formObj.DoctorInfo }).pipe(take(1)).subscribe(res => {
      this.router.navigate(["/account/send-email-verification"], { state: formObj.AccountInfo?.email as String });
    });
  }
}
