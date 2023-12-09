import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Article } from '../models/article';
import { Filter } from '../models/filter';
import { PaginatedList } from '../models/paginated-list';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {
  baseUrl = environment.baseUrl + '/articles';
  constructor(private httpClient: HttpClient) { }

  getById(id: number) {
    return this.httpClient.get<Article>(this.baseUrl + '/' + id);
  }

  getArticles(articleFilter: Filter) {
    let params = new HttpParams();
    params = articleFilter.title ? params.append('title', articleFilter.title) : params;
    params = params.append('pageNumber', articleFilter.pageNumber);
    params = params.append('pageSize', articleFilter.pageSize);

    return this.httpClient.get<PaginatedList<Article>>(this.baseUrl, { params });
  }
}
