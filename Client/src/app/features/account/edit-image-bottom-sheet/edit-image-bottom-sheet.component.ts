import { Component } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/services/account.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-edit-image-bottom-sheet',
  templateUrl: './edit-image-bottom-sheet.component.html',
  styleUrls: ['./edit-image-bottom-sheet.component.css']
})
export class EditImageBottomSheetComponent {

  result: {
    removeImage: boolean,
    editImage: boolean,
    showImage: boolean
  } = {
      removeImage: false,
      editImage: false,
      showImage: false
    }

  constructor(private bottomSheetRef: MatBottomSheetRef<EditImageBottomSheetComponent>) { }

  removeImage() {
    this.result.removeImage = true;
    this.bottomSheetRef.dismiss(this.result);

  }

  uploadImage() {
    this.result.editImage = true;
    this.bottomSheetRef.dismiss(this.result);
  }

  showImage() {
    this.result.showImage = true;
    this.bottomSheetRef.dismiss(this.result);
  }
}
