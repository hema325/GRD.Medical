import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { PaginatedList } from 'src/app/models/paginated-list';
import { Post } from 'src/app/models/posts/post';
import { PostFilter } from 'src/app/models/post-filter';
import { PostsService } from 'src/app/services/posts.service';
import { AuthResult } from 'src/app/models/account/auth-result';
import { AccountService } from 'src/app/services/account.service';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { EditPostBottomSheetComponent } from '../edit-post-bottom-sheet/edit-post-bottom-sheet.component';
import { environment } from 'src/environments/environment.development';
import { Media } from 'src/app/models/media';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-posts-list',
  templateUrl: './posts-list.component.html',
  styleUrls: ['./posts-list.component.css']
})
export class PostsListComponent {
  defaultUserImageUrl = environment.defaultUserImageUrl;
  preventLoading = false;
  paginatedList?: PaginatedList<Post>;
  commentSections: boolean[] = [false]
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
    this.getPosts();
    this.addCreatedPosts();
    this.loadCurrentAuth();
    this.loadPostsWhenReachedBottom();
  }

  loadPostsWhenReachedBottom() {
    window.addEventListener('scroll', () => this.isPostsContainerAtBottom() && !this.preventLoading ? this.loadMorePosts() : null)

  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

  getMediaUrls(medias: Media[]) {
    return medias.map(m => m.url);
  }

  openPostBottomSheet(postId: number) {
    const bottomSheet = this.postBottomSheet.open(EditPostBottomSheetComponent, { data: postId });
    bottomSheet.afterDismissed().subscribe(res => {
      if (res.postDeleted)
        this.removePost(postId);
    });
  }

  removePost(postId: number) {
    if (!this.paginatedList)
      return;

    const posts = this.paginatedList.data;
    const postIndex = posts.findIndex(p => p.id == postId);
    posts.splice(postIndex, 1);
    this.commentSections.splice(postIndex, 1);
  }

  getPosts() {
    this.preventLoading = true;
    this.postsService.get(this.postFilter)
      .pipe(finalize(() => this.preventLoading = false))
      .subscribe(res => {
        if (this.paginatedList)
          res.data.unshift(...this.paginatedList.data);

        this.paginatedList = res
      });
  }

  loadMorePosts() {
    if (this.paginatedList?.hasNextPage) {
      this.postFilter.pageNumber += 1;
      this.getPosts();
    }
  }

  addCreatedPosts() {
    this.postsService.createdPost$.subscribe(post => {
      if (post && this.paginatedList) {
        this.paginatedList.data.unshift(post);
        this.commentSections.unshift(false);
      }
    });
  }

  @ViewChild('postsContainer') postsContainer?: ElementRef;

  isPostsContainerAtBottom(): boolean {
    const container = this.postsContainer?.nativeElement;
    if (!container) return false;

    const containerRect = container.getBoundingClientRect();
    const windowHeight = window.innerHeight || document.documentElement.clientHeight;
    const containerBottom = containerRect.bottom;

    return containerBottom <= windowHeight;
  }

  ngOnDestroy() {
    this.preventLoading = true;
  }
}
