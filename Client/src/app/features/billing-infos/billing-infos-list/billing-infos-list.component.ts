import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { finalize } from 'rxjs';
import { AuthResult } from 'src/app/models/account/auth-result';
import { Roles } from 'src/app/models/account/roles.enum';
import { BillingInfo } from 'src/app/models/billing-info';
import { FilterBase } from 'src/app/models/filter-base';
import { PaginatedList } from 'src/app/models/paginated-list';
import { AccountService } from 'src/app/services/account.service';
import { BillingInfosService } from 'src/app/services/billing-infos.service';

@Component({
  selector: 'app-billing-infos-list',
  templateUrl: './billing-infos-list.component.html',
  styleUrls: ['./billing-infos-list.component.css']
})
export class BillingInfosListComponent {
  displayedColumns: string[] = ['date', 'time', 'amount'];

  billingInfos: PaginatedList<BillingInfo> | null = null;
  billingInfosFilter: FilterBase = {
    pageNumber: 1,
    pageSize: 10
  }
  isLoading = false;

  currentAuth: AuthResult | null = null;

  constructor(private billingInfosService: BillingInfosService,
    private accountService: AccountService) { }

  ngOnInit() {
    this.loadbillingInfos();
    this.loadCurrentAuth();
  }

  isDoctor() {
    return this.currentAuth?.role == Roles.Doctor;
  }

  isPatient() {
    return this.currentAuth?.role == Roles.Patient;
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

  loadbillingInfos() {
    this.isLoading = true;
    this.billingInfosService.get(this.billingInfosFilter)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe(res => {
        this.billingInfos = res;
      });
  }

  handlePageEvent(event: PageEvent) {
    this.billingInfosFilter.pageSize = event.pageSize;
    this.billingInfosFilter.pageNumber = event.pageIndex + 1;
    this.loadbillingInfos();
  }
}
