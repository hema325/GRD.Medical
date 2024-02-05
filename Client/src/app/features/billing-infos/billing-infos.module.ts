import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BillingInfosListComponent } from './billing-infos-list/billing-infos-list.component';
import { SharedModule } from 'src/app/shared/shared.module';



@NgModule({
  declarations: [
    BillingInfosListComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    BillingInfosListComponent
  ]
})
export class BillingInfosModule { }
