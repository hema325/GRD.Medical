import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { FilterBase } from '../models/filter-base';
import { PaginatedList } from '../models/paginated-list';
import { Appointment } from '../models/appointments/appointment';

@Injectable({
  providedIn: 'root'
})
export class AppointmentsService {

  baseUrl = environment.baseUrl + '/appointments';

  constructor(private httpClient: HttpClient) { }

  create(data: any) {
    return this.httpClient.post<Number>(this.baseUrl, data);
  }

  get(filter: FilterBase) {
    let params = new HttpParams();
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<Appointment>>(this.baseUrl, { params });
  }

  getById(id: number) {
    return this.httpClient.get<Appointment>(this.baseUrl + '/' + id);
  }

  markCompleted(id: number) {
    return this.httpClient.post(this.baseUrl + '/markCompleted', { id: id });
  }
}
