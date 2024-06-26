import { Component, Input } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/users/user';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-update-user-details',
  templateUrl: './update-user-details.component.html',
  styleUrls: ['./update-user-details.component.css']
})
export class UpdateAccountDetailsComponent {

  @Input() user: User | null = null;

  userForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(20)]],
    lastName: ['', [Validators.required, Validators.maxLength(20)]]
  });

  constructor(private accountService: AccountService,
    private fb: FormBuilder,
    private toastr: ToastrService) { }

  ngOnChanges() {
    if (this.user)
      this.userForm.patchValue(this.user);
  }

  updateDetails() {
    this.accountService.updateUser(this.userForm.value).subscribe(res => {
      if (this.user) {
        this.user.firstName = this.userForm.value.firstName!;
        this.user.lastName = this.userForm.value.lastName!;
      }
      this.toastr.success('Updates saved successfully');
    });
  }

}
