import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Comment } from 'src/app/models/comments/comment';
import { CreateComment } from '../models/comments/create-comment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CommentFilter } from '../models/comments/comment-filter';
import { PaginatedList } from '../models/paginated-list';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  baseUrl = environment.baseUrl + '/comments';

  private commentCreated = new BehaviorSubject<Comment | null>(null);
  commentCreated$ = this.commentCreated.asObservable();

  constructor(private httpClient: HttpClient) { }

  create(comment: CreateComment) {
    const form = new FormData();
    comment.content ? form.append('content', comment.content) : null;
    comment.image ? form.append('image', comment.image) : null;
    comment.replyTo ? form.append('replyTo', comment.replyTo.toString()) : null;
    form.append('postId', comment.postId.toString());

    return this.httpClient.post<Comment>(this.baseUrl, form).pipe(map(comment => {
      this.commentCreated.next(comment);
      return comment;
    }));
  }

  delete(id: number) {
    return this.httpClient.delete(this.baseUrl + '/' + id);
  }

  get(filter: CommentFilter) {
    let params = new HttpParams();
    params = filter.replyTo ? params.append('replyTo', filter.replyTo.toString()) : params;
    params = filter.before ? params.append('before', filter.before) : params;
    params = params.append('postId', filter.postId);
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<Comment>>(this.baseUrl, { params });
  }
}
