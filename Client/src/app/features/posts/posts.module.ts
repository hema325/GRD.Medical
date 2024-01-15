import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostsComponent } from './posts.component';
import { PostsRoutingModule } from './posts-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CoreModule } from 'src/app/core/core.module';
import { CreatePostComponent } from './create-post/create-post.component';
import { FormsModule } from '@angular/forms';
import { CreateCommentComponent } from './comments/create-comment/create-comment.component';
import { PostsListComponent } from './posts-list/posts-list.component';
import { CommentsListComponent } from './comments/comments-list/comments-list.component';
import { EditPostBottomSheetComponent } from './edit-post-bottom-sheet/edit-post-bottom-sheet.component';
import { EditCommentBottomSheetComponent } from './comments/edit-comment-bottom-sheet/edit-comment-bottom-sheet.component';
import { PostCardComponent } from './post-card/post-card.component';
import { PostsForCommentComponent } from './posts-for-comment/posts-for-comment.component';



@NgModule({
  declarations: [
    PostsComponent,
    CreatePostComponent,
    CreateCommentComponent,
    PostsListComponent,
    CommentsListComponent,
    EditPostBottomSheetComponent,
    EditCommentBottomSheetComponent,
    PostCardComponent,
    PostsForCommentComponent
  ],
  imports: [
    CommonModule,
    PostsRoutingModule,
    SharedModule,
    CoreModule,
    FormsModule
  ],
  exports: [
    CreatePostComponent,
    PostsListComponent
  ]
})
export class PostsModule { }
