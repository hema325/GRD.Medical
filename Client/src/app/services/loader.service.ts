import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  private activeRequests = new Set();
  private activeRequestsCount = 0;
  private skipNext = false;

  constructor(private spinner: NgxSpinnerService) { }

  skipNextRequest() {
    this.skipNext = true;
  }

  show(id: string) {

    if (this.skipNext) {
      this.skipNext = false;
      return;
    }

    if (!this.activeRequestsCount)
      this.spinner.show();

    this.activeRequests.add(id);
    this.activeRequestsCount += 1;
  }

  hide(id: string) {
    if (this.activeRequests.has(id)) {

      this.activeRequests.delete(id);
      this.activeRequestsCount -= 1;

      if (this.activeRequestsCount == 0)
        this.spinner.hide();
    }
  }


}
