import { Component, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { User } from 'src/app/models/account/user';
import { AccountService } from 'src/app/services/account.service';
import { EditImageBottomSheetComponent } from './edit-image-bottom-sheet/edit-image-bottom-sheet.component';


@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  user: User | null = null;
  tab = 0;

  constructor(private accountService: AccountService,
    private bottomSheet: MatBottomSheet) { }

  ngOnInit() {
    this.accountService.getDetails().subscribe(data => this.user = data);
  }

  openBottomSheet() {
    let sheet = this.bottomSheet.open(EditImageBottomSheetComponent);
    sheet.afterDismissed().subscribe(res => {
      if (this.user && res)
        this.user.imageUrl = res
    });
  }

}
