import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { SpecialistFilter } from '../models/specialists/specialist-filter';
import { Speciality } from '../models/specialists/Speciality';
import { PaginatedList } from '../models/paginated-list';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SpecialistsService {

  baseUrl = environment.baseUrl + '/specialities';

  constructor(private httpClient: HttpClient) { }

  get(filter: SpecialistFilter) {
    let params = new HttpParams();
    params = filter.name ? params.append("name", filter.name) : params;
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<Speciality>>(this.baseUrl, { params });
  }
}
