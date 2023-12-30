import { Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { User } from 'src/app/models/account/user';
import { AccountService } from 'src/app/services/account.service';
import { EditImageBottomSheetComponent } from './edit-image-bottom-sheet/edit-image-bottom-sheet.component';
import { environment } from 'src/environments/environment.development';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  defaultUserImageUrl = environment.defaultUserImageUrl;
  user: User | null = null;
  tab = 0;
  profieImageActive = false;

  constructor(private accountService: AccountService,
    private bottomSheet: MatBottomSheet,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.accountService.getDetails().subscribe(data => this.user = data);
  }


  @ViewChild('imageInput') imageInput?: ElementRef;

  openBottomSheet() {
    let sheet = this.bottomSheet.open(EditImageBottomSheetComponent);
    sheet.afterDismissed().subscribe(res => {
      if (res.removeImage)
        this.removeImage();
      else if (res.editImage)
        this.imageInput?.nativeElement.click();
      else if (res.showImage)
        this.profieImageActive = true;
    });
  }

  removeImage() {
    this.accountService.removeImage().subscribe(res => {
      this.toastr.success('Profile image removed successfully.');
      if (this.user)
        this.user.imageUrl = this.defaultUserImageUrl;
    });
  }

  uploadImage(e: any) {
    const image = e.target.files[0];
    if (image) {
      this.accountService.uploadImage(e.target.files[0]).subscribe(res => {
        this.toastr.success('Profile image updated successfully.');
        if (this.user)
          this.user.imageUrl = res.url;
      });
    }
  }


}
