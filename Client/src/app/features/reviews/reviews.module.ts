import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReviewsRoutingModule } from './reviews-routing.module';
import { CreateReviewComponent } from './create-review/create-review.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReviewsListComponent } from './reviews-list/reviews-list.component';



@NgModule({
  declarations: [
    CreateReviewComponent,
    ReviewsListComponent
  ],
  imports: [
    CommonModule,
    ReviewsRoutingModule,
    SharedModule
  ],
  exports: [
    ReviewsListComponent
  ]
})
export class ReviewsModule { }
