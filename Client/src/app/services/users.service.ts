import { Injectable } from '@angular/core';
import { User } from '../models/users/user';
import { environment } from 'src/environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DoctorFilter } from '../models/users/doctor-filter';
import { PaginatedList } from '../models/paginated-list';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseUrl = environment.baseUrl + '/users';

  constructor(private httpClient: HttpClient) { }

  getUser(id: number) {
    return this.httpClient.get<User>(this.baseUrl + '/' + id);
  }

  getDoctors(filter: DoctorFilter) {
    let params = new HttpParams();
    params = filter.name ? params.append('name', filter.name) : params;
    params = filter.experience ? params.append('experience', filter.experience) : params;
    params = filter.specialityId ? params.append('specialityId', filter.specialityId) : params;
    params = filter.orderBy ? params.append('orderBy', filter.orderBy) : params;
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<User>>(this.baseUrl + '/doctors', { params });
  }

}
