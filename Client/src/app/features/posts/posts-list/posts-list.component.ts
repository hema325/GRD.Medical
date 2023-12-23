import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { PaginatedList } from 'src/app/models/paginated-list';
import { Post } from 'src/app/models/posts/post';
import { PostFilter } from 'src/app/models/post-filter';
import { PostsService } from 'src/app/services/posts.service';

@Component({
  selector: 'app-posts-list',
  templateUrl: './posts-list.component.html',
  styleUrls: ['./posts-list.component.css']
})
export class PostsListComponent {

  preventLoading = false;
  paginatedList?: PaginatedList<Post>;
  commentSections: boolean[] = [false]

  postFilter: PostFilter = {
    ownerId: null,
    pageNumber: 1,
    pageSize: 6
  }


  constructor(private postsService: PostsService) { }

  ngOnInit() {
    this.getPosts();
    this.addCreatedPosts();
    window.addEventListener('scroll', () => this.isPostsContainerAtBottom() && !this.preventLoading ? this.loadMorePosts() : null)
  }

  getPosts() {
    this.preventLoading = true;
    this.postsService.get(this.postFilter).subscribe({
      next: res => {
        if (this.paginatedList)
          res.data.unshift(...this.paginatedList.data);

        this.paginatedList = res
        this.preventLoading = false;
      },
      error: error => this.preventLoading = false
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
        this.paginatedList.data = [post, ...this.paginatedList!.data.slice(0, this.paginatedList.pageSize - 1)];
        this.commentSections = [false, ...this.commentSections.slice(0, this.paginatedList.pageSize - 1)];

        this.postFilter.pageNumber = 1;
        this.paginatedList.pageNumber = 1;
        this.paginatedList.hasNextPage = this.paginatedList.totalCount > this.paginatedList.pageSize - 1;
        this.paginatedList.hasPreviousPage = false;
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
