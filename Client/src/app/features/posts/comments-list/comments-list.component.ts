import { Component, Input } from '@angular/core';
import { Comment } from 'src/app/models/comments/comment';
import { PaginatedList } from 'src/app/models/paginated-list';
import { CommentsService } from 'src/app/services/comments.service';

@Component({
  selector: 'app-comments-list',
  templateUrl: './comments-list.component.html',
  styleUrls: ['./comments-list.component.css']
})
export class CommentsListComponent {

  @Input() postId: number = 0;

  commentFilter = {
    pageNumber: 1,
    pageSize: 6
  }

  paginatedList: PaginatedList<Comment> | null = null;

  constructor(private commentsService: CommentsService) { }

  ngOnInit() {
    this.loadComments();
    this.addCreatedComment();
  }

  loadComments() {
    this.commentsService.get({
      replyTo: null,
      postId: this.postId,
      pageNumber: this.commentFilter.pageNumber,
      pageSize: this.commentFilter.pageSize
    }).subscribe(comments => {
      if (this.paginatedList)
        comments.data.unshift(...this.paginatedList.data);
      this.paginatedList = comments;
    });
  }


  loadMoreComments() {
    if (this.paginatedList?.hasNextPage) {
      this.commentFilter.pageNumber += 1;
      this.loadComments();
    }
  }


  addCreatedComment() {
    this.commentsService.commentCreated$.subscribe(comment => {
      if (this.paginatedList && comment && comment.postId == this.postId) {
        if (!comment.replyTo) {
          this.paginatedList.data = [comment, ...this.paginatedList.data.slice(0, this.paginatedList.pageSize - 1)]
        }
        else {
          const mainComment = this.paginatedList.data.find(c => c.id == comment.replyTo);
          if (mainComment) {
            mainComment.replies = [comment, ...mainComment.replies.slice(0, this.commentFilter.pageSize - 1)]
            mainComment.totalRepliesCount += 1;
          }
        }
        this.commentFilter.pageNumber = 1;
        this.paginatedList.pageNumber = 1;
        this.paginatedList.hasNextPage = this.paginatedList.totalCount > this.paginatedList.pageSize - 1;
        this.paginatedList.hasPreviousPage = false;
      }
    })
  }


  loadMoreReplies(comment: Comment) {
    if (comment.replies.length < comment.totalRepliesCount) {
      this.commentsService.get({
        replyTo: comment.id,
        postId: this.postId,
        pageNumber: Math.ceil(comment.replies.length / this.commentFilter.pageSize) + (comment.replies.length < this.commentFilter.pageSize ? 0 : 1),
        pageSize: this.commentFilter.pageSize
      }).subscribe(comments => {
        if (comment.replies.length < this.commentFilter.pageSize)
          comment.replies = comments.data;
        else
          comment.replies.push(...comments.data);
      });
    }
  }

}
