import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostsComponent } from './posts.component';
import { PostsRoutingModule } from './posts-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CoreModule } from 'src/app/core/core.module';
import { BytesSizePipe } from 'src/app/pipes/bytes-size.pipe';
import { CreatePostComponent } from './create-post/create-post.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PostCardComponent } from './post-card/post-card.component';
import { CreateCommentComponent } from './create-comment/create-comment.component';



@NgModule({
  declarations: [
    PostsComponent,
    CreatePostComponent,
    PostCardComponent,
    CreateCommentComponent
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
