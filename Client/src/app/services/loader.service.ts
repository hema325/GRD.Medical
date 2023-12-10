import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  private activeRequestsCount = 0;
  private isActive = true;

  constructor(private spinner: NgxSpinnerService) { }

  activate() {
    this.isActive = true;
  }

  deactivate() {
    this.isActive = false;
  }

  show() {
    if (this.isActive) {
      ++this.activeRequestsCount;
      this.spinner.show();
    }
  }

  hide() {
    --this.activeRequestsCount;
    if (this.activeRequestsCount <= 0) {
      this.spinner.hide();
      this.activeRequestsCount = 0;
    }
  }
}
