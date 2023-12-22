import { Component } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-edit-image',
  templateUrl: './edit-image.component.html',
  styleUrls: ['./edit-image.component.css']
})
export class EditImageComponent {


  constructor(private accountService: AccountService,
    private toastr: ToastrService,
    private bottomSheetRef: MatBottomSheetRef<EditImageComponent>) { }

  removeImage() {
    this.accountService.removeImage().subscribe(res => {
      this.toastr.success('Profile image removed successfully.');
      this.bottomSheetRef.dismiss('assets/images/profile.png');
    });
  }

  uploadImage(event: any) {
    var fd = new FormData();
    fd.append('image', event.target.files[0]);
    this.accountService.uploadImage(fd).subscribe(res => {
      this.toastr.success('Profile image updated successfully.');
      this.bottomSheetRef.dismiss(res.url);
    });

  }
}
