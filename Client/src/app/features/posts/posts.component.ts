import { Component, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { PaginatedList } from 'src/app/models/paginated-list';
import { Post } from 'src/app/models/posts/post';
import { PostFilter } from 'src/app/models/posts/post-filter';
import { PostsService } from 'src/app/services/posts.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent {

}
