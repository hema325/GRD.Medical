import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { PaymentIntent } from '../models/billingInfos/payment-intent';
import { FilterBase } from '../models/filter-base';
import { PaginatedList } from '../models/paginated-list';
import { BillingInfo } from '../models/billing-info';

@Injectable({
  providedIn: 'root'
})
export class BillingInfosService {

  baseUrl = environment.baseUrl + '/billings';

  constructor(private httpClient: HttpClient) { }

  createPaymentIntentId(doctorId: string) {
    return this.httpClient.post<PaymentIntent>(this.baseUrl + '/createPaymentIntentId', { doctorId: doctorId });
  }

  get(filter: FilterBase) {
    let params = new HttpParams();
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<BillingInfo>>(this.baseUrl, { params });
  }
}
