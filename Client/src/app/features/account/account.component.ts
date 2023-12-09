<<<<<<< HEAD
import { Component } from '@angular/core';
=======
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { password } from 'src/app/validators/password.validator';
import { EditImageComponent } from './edit-image/edit-image.component';
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
<<<<<<< HEAD
export class AccountComponent {
=======
export class AccountComponent implements OnInit {

  user: User | null = null;
  tab = 0;

  userForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(20)]],
    lastName: ['', [Validators.required, Validators.maxLength(20)]]
  });

  passwordForm = this.fb.group({
    oldPassword: ['', Validators.required],
    newPassword: ['', [Validators.required, password()]],
  })

  constructor(private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private bottomSheet: MatBottomSheet) { }

  ngOnInit() {
    this.accountService.getDetails().subscribe(data => {
      this.userForm.setValue({
        firstName: data.firstName,
        lastName: data.lastName
      });
      this.user = data;
    });
  }

  updateDetails() {
    this.accountService.update(this.userForm.value).subscribe(res => {
      if (this.user) {
        this.user.firstName = this.userForm.value.firstName!;
        this.user.lastName = this.userForm.value.lastName!;
      }
      this.toastr.success('Updates saved successfully');
    });
  }

  changePassword() {
    this.accountService.changePassword(this.passwordForm.value).subscribe({
      next: res => {
        this.router.navigateByUrl("/account/login");
        this.accountService.logout().subscribe();
        this.toastr.success('Password changed successfully');
      }
    })
  }

  openBottomSheet() {
    let sheet = this.bottomSheet.open(EditImageComponent);
    sheet.afterDismissed().subscribe(res => {
      if (this.user && res)
        this.user.imageUrl = res
    });
  }
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf

}
