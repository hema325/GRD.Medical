import { Injectable } from '@angular/core';
import { FilterBase } from '../models/filter-base';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginatedList } from '../models/paginated-list';
import { environment } from 'src/environments/environment.development';
import { Language } from '../models/language';

@Injectable({
  providedIn: 'root'
})
export class LanguagesService {

  baseUrl = environment.baseUrl + '/languages';

  constructor(private httpClient: HttpClient) { }

  get(filter: FilterBase) {
    let params = new HttpParams();
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<Language>>(this.baseUrl, { params });
  }
}
