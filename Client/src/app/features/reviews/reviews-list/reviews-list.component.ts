import { Component, Input } from '@angular/core';
import { finalize } from 'rxjs';
import { AuthResult } from 'src/app/models/account/auth-result';
import { PaginatedList } from 'src/app/models/paginated-list';
import { Review } from 'src/app/models/reviews/review';
import { ReviewFilter } from 'src/app/models/reviews/review-filter';
import { AccountService } from 'src/app/services/account.service';
import { ReviewsService } from 'src/app/services/reviews.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-reviews-list',
  templateUrl: './reviews-list.component.html',
  styleUrls: ['./reviews-list.component.css']
})
export class ReviewsListComponent {
  defaultUserImageUrl = environment.defaultUserImageUrl;

  @Input() doctorId: number = 0;

  currentAuth: AuthResult | null = null;
  reviews: PaginatedList<Review> | null = null;
  isFirstLoading = true;
  isLoading = false;

  constructor(private reviewsService: ReviewsService,
    private accountService: AccountService) { }

  filter: ReviewFilter = {
    doctorId: 0,
    pageNumber: 1,
    pageSize: 10
  }

  ngOnChanges() {
    this.filter.doctorId = this.doctorId;
  }

  ngOnInit() {
    this.loadReviews();
    this.loadCurrentAuth();
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

  loadReviews() {
    this.isLoading = true;
    this.reviewsService.get(this.filter)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.isFirstLoading = false;
      }))
      .subscribe(res => {
        if (this.reviews)
          res.data.unshift(...this.reviews.data);

        this.reviews = res
      });
  }

  loadMoreReviews() {
    if (this.reviews?.hasNextPage && !this.isLoading) {
      this.filter.pageNumber += 1;
      this.loadReviews();
    }
  }
}
