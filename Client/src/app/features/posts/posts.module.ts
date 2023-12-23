import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostsComponent } from './posts.component';
import { PostsRoutingModule } from './posts-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CoreModule } from 'src/app/core/core.module';
import { CreatePostComponent } from './create-post/create-post.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateCommentComponent } from './create-comment/create-comment.component';
import { PostsListComponent } from './posts-list/posts-list.component';
import { CommentsListComponent } from './comments-list/comments-list.component';



@NgModule({
  declarations: [
    PostsComponent,
    CreatePostComponent,
    CreateCommentComponent,
    PostsListComponent,
    CommentsListComponent
  ],
  imports: [
    CommonModule,
    PostsRoutingModule,
    SharedModule,
    CoreModule,
    FormsModule
  ]
})
export class PostsModule { }
