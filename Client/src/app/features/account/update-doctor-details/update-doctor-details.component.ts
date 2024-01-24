import { Component, Input } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/users/user';
import { AccountService } from 'src/app/services/account.service';
import { precisionScale } from 'src/app/validators/precision-scale.validator';

@Component({
  selector: 'app-update-doctor-details',
  templateUrl: './update-doctor-details.component.html',
  styleUrls: ['./update-doctor-details.component.css']
})
export class UpdateDoctorDetailsComponent {
  @Input() user: User | null = null;

  doctorForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(20)]],
    lastName: ['', [Validators.required, Validators.maxLength(20)]],
    experience: ['', [Validators.required, Validators.max(60)]],
    consultFee: ['', [Validators.required, precisionScale(9, 2)]],
    specialityId: ['', [Validators.required]],
    languageIds: [[''], [Validators.required]],
    biography: ['', [Validators.required]]
  })

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private toastr: ToastrService) { }

  ngOnChanges() {
    this.updateDoctorForm();
  }

  updateDoctorForm() {
    if (this.user)
      this.doctorForm.patchValue({
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        experience: this.user.doctor.experience.toString(),
        consultFee: this.user.doctor.consultFee.toString(),
        biography: this.user.doctor.biography,
        specialityId: this.user.doctor.speciality.id.toString(),
        languageIds: this.user.doctor.languages.map(l => l.id.toString())
      });
  }

  updateDetails() {
    this.accountService.updateDoctor(this.doctorForm.value).subscribe(() => {
      if (this.user) {
        this.user.firstName = this.doctorForm.value.firstName!;
        this.user.lastName = this.doctorForm.value.lastName!;
      }
      this.toastr.success('Updates saved successfully');
    })
  }
}
