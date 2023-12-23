import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Advice } from '../models/advices/advice';
import { PaginatedList } from '../models/paginated-list';
import { AdviceFilter } from '../models/advices/advice-filter';

@Injectable({
  providedIn: 'root'
})
export class AdvicesService {

  baseUrl = environment.baseUrl + '/advices';

  constructor(private httpClient: HttpClient) { }

  getById(id: number) {
    return this.httpClient.get<Advice>(this.baseUrl + '/' + id);
  }

  getAdvices(filter: AdviceFilter) {
    var params = new HttpParams();
    params = filter.title ? params.append('title', filter.title) : params;
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<Advice>>(this.baseUrl, { params });
  }
}
