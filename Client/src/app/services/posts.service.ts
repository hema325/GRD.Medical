import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Post } from '../models/posts/post';
import { PaginatedList } from '../models/paginated-list';
import { PostFilter } from '../models/post-filter';
import { BehaviorSubject, map } from 'rxjs';
import { CreatePost } from '../models/posts/create-post';

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  baseUrl = environment.baseUrl + '/posts';

  private createdPost = new BehaviorSubject<Post | null>(null);
  createdPost$ = this.createdPost.asObservable();

  constructor(private httpClient: HttpClient) { }

  create(data: CreatePost) {
    const form = new FormData();
    form.append('content', data.content);
    data.images?.forEach(image => form.append('images', image));

    return this.httpClient.post<Post>(this.baseUrl, form).pipe(map(post => {
      this.createdPost.next(post);
      return post;
    }));
  }

  delete(id: number) {
    return this.httpClient.delete(this.baseUrl + '/' + id);
  }


  get(filter: PostFilter) {
    let params = new HttpParams();
    params = filter.ownerId ? params.append('ownerId', filter.ownerId) : params;
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);
    params = filter.before ? params.append('before', filter.before) : params;

    return this.httpClient.get<PaginatedList<Post>>(this.baseUrl, { params });
  }
}
