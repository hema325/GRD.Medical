import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Filter } from '../models/filter';
import { Advice } from '../models/advice';
import { PaginatedList } from '../models/paginated-list';

@Injectable({
  providedIn: 'root'
})
export class AdvicesService {

  baseUrl = environment.baseUrl + '/advices';

  constructor(private httpClient: HttpClient) { }

  getById(id: number) {
    return this.httpClient.get<Advice>(this.baseUrl + '/' + id);
  }

  getAdvices(filter: Filter) {
    var params = new HttpParams();
    params = filter.title ? params.append('title', filter.title) : params;
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<Advice>>(this.baseUrl, { params });
  }
}
