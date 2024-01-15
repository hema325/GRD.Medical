import { Component, Input } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { Comment } from 'src/app/models/comments/comment';
import { PaginatedList } from 'src/app/models/paginated-list';
import { CommentsService } from 'src/app/services/comments.service';
import { EditCommentBottomSheetComponent } from '../edit-comment-bottom-sheet/edit-comment-bottom-sheet.component';
import { AuthResult } from 'src/app/models/account/auth-result';
import { AccountService } from 'src/app/services/account.service';
import { environment } from 'src/environments/environment.development';
import { CommentFilter } from 'src/app/models/comments/comment-filter';

@Component({
  selector: 'app-comments-list',
  templateUrl: './comments-list.component.html',
  styleUrls: ['./comments-list.component.css']
})
export class CommentsListComponent {

  defaultUserImageUrl = environment.defaultUserImageUrl;
  @Input() postId: number = 0;

  commentFilter: CommentFilter = {
    pageNumber: 1,
    pageSize: 6,
    before: new Date().toISOString(),
    replyTo: null,
    postId: 0
  }

  currentAuth: AuthResult | null = null;

  paginatedList: PaginatedList<Comment> | null = null;

  constructor(private commentsService: CommentsService,
    private commentBottomSheet: MatBottomSheet,
    private accountService: AccountService) { }

  ngOnInit() {
    this.commentFilter.postId = this.postId; //must be at first
    this.loadComments();
    this.addCreatedComment();
    this.loadCurrentAuth();
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

  openCommentBottomSheet(comment: Comment) {
    const bottomSheet = this.commentBottomSheet.open(EditCommentBottomSheetComponent, { data: comment.id });
    bottomSheet.afterDismissed().subscribe(res => {
      if (res.commentDeleted)
        this.removeComment(comment);
    });
  }

  removeComment(comment: Comment) {
    if (!this.paginatedList)
      return;

    const comments = this.paginatedList.data;
    if (comment.replyTo) {
      const mainComment = comments.find(c => c.id == comment.replyTo);
      if (mainComment) {
        mainComment.replies.splice(mainComment.replies.findIndex(r => r.id == comment.id), 1);
        mainComment.totalRepliesCount -= 1;
      }
    }
    else
      comments.splice(comments.findIndex(c => c.id == comment.id), 1);
  }

  loadComments() {
    this.commentsService.get(this.commentFilter)
      .subscribe(comments => {
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
        if (!comment.replyTo)
          this.paginatedList.data.unshift(comment);
        else {
          const mainComment = this.paginatedList.data.find(c => c.id == comment.replyTo);
          if (mainComment) {
            mainComment.replies.push(comment);
            mainComment.totalRepliesCount += 1;
          }
          this.commentFilter.pageNumber = 1;
          this.paginatedList.pageNumber = 1;
          this.paginatedList.hasNextPage = this.paginatedList.totalCount > this.paginatedList.pageSize - 1;
          this.paginatedList.hasPreviousPage = false;
        }
      }
    })
  }


  loadMoreReplies(comment: Comment) {
    if (comment.replies.length < comment.totalRepliesCount) {
      this.commentsService.get({
        replyTo: comment.id,
        postId: this.postId,
        pageNumber: Math.ceil(comment.replies.length / this.commentFilter.pageSize) + (comment.replies.length < this.commentFilter.pageSize ? 0 : 1),
        pageSize: this.commentFilter.pageSize,
        before: null
      }).subscribe(comments => {
        if (comment.replies.length < this.commentFilter.pageSize)
          comment.replies = comments.data;
        else
          comment.replies.push(...comments.data);
      });
    }
  }

}
