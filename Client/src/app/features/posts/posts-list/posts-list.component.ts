import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { PaginatedList } from 'src/app/models/paginated-list';
import { Post } from 'src/app/models/posts/post';
import { PostFilter } from 'src/app/models/posts/post-filter';
import { PostsService } from 'src/app/services/posts.service';
import { AuthResult } from 'src/app/models/account/auth-result';
import { AccountService } from 'src/app/services/account.service';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-posts-list',
  templateUrl: './posts-list.component.html',
  styleUrls: ['./posts-list.component.css']
})
export class PostsListComponent {
  isLoading = false;
  paginatedList?: PaginatedList<Post>;
  currentAuth: AuthResult | null = null;

  @Input() ownerId: number | null = null;

  postFilter: PostFilter = {
    ownerId: null,
    pageNumber: 1,
    pageSize: 6,
    before: new Date().toISOString()
  }

  constructor(private postsService: PostsService,
    private accountService: AccountService,
    private postBottomSheet: MatBottomSheet) { }

  ngOnChanges() {
    this.postFilter.ownerId = this.ownerId;
  }

  ngOnInit() {
    this.loadPosts();
    this.addCreatedPosts();
    this.loadCurrentAuth();
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

  openPostBottomSheet(postId: number) {
    this.removePost(postId);
  }

  removePost(postId: number) {
    if (!this.paginatedList)
      return;

    const posts = this.paginatedList.data;
    const postIndex = posts.findIndex(p => p.id == postId);
    posts.splice(postIndex, 1);
  }

  loadPosts() {
    this.isLoading = true;
    this.postsService.get(this.postFilter)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe(res => {
        if (this.paginatedList)
          res.data.unshift(...this.paginatedList.data);

        this.paginatedList = res
      });
  }

  loadMorePosts() {
    if (this.paginatedList?.hasNextPage && !this.isLoading) {
      this.postFilter.pageNumber += 1;
      this.loadPosts();
    }
  }

  addCreatedPosts() {
    this.postsService.createdPost$.subscribe(post => {
      if (post && this.paginatedList) {
        this.paginatedList.data.unshift(post);
      }
    });
  }
}
