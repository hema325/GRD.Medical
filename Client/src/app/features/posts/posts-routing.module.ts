import { NgModule } from '@angular/core';
import { PostsComponent } from './posts.component';
import { RouterModule } from '@angular/router';
import { PostsForCommentComponent } from './posts-for-comment/posts-for-comment.component';
import { PostDetailsComponent } from './post-details/post-details.component';


const routes = [
  { path: '', component: PostsComponent },
  { path: ':id', component: PostDetailsComponent },
  { path: 'post-for-comment/:id', component: PostsForCommentComponent },
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class PostsRoutingModule { }
