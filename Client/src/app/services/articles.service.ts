import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Article } from '../models/articles/article';
import { PaginatedList } from '../models/paginated-list';
import { ArticleFilter } from '../models/articles/article-filter';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {
  baseUrl = environment.baseUrl + '/articles';
  constructor(private httpClient: HttpClient) { }

  getById(id: number) {
    return this.httpClient.get<Article>(this.baseUrl + '/' + id);
  }

  getArticles(filter: ArticleFilter) {
    let params = new HttpParams();
    params = filter.title ? params.append('title', filter.title) : params;
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<Article>>(this.baseUrl, { params });
  }
}
