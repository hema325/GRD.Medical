import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { ReviewFilter } from '../models/reviews/review-filter';
import { PaginatedList } from '../models/paginated-list';
import { Review } from '../models/reviews/review';

@Injectable({
  providedIn: 'root'
})
export class ReviewsService {

  baseUrl = environment.baseUrl + '/reviews';

  constructor(private httpClient: HttpClient) { }


  create(data: any) {
    return this.httpClient.post<number>(this.baseUrl, data);
  }

  get(filter: ReviewFilter) {
    let params = new HttpParams();
    params = params.append('doctorId', filter.doctorId);
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<Review>>(this.baseUrl, { params })
  }
}
