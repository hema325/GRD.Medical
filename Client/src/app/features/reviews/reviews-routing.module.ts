import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CreateReviewComponent } from './create-review/create-review.component';


const routes = [
  { path: 'create-review', component: CreateReviewComponent }
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ReviewsRoutingModule { }
