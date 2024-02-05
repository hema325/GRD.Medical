import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReviewsService } from 'src/app/services/reviews.service';

@Component({
  selector: 'app-create-review',
  templateUrl: './create-review.component.html',
  styleUrls: ['./create-review.component.css']
})
export class CreateReviewComponent {

  reviewForm = this.fb.group({
    stars: ['', Validators.required],
    content: ['', [Validators.required, Validators.maxLength(500)]],
    doctorId: ['', Validators.required]
  })

  constructor(private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private reviewsService: ReviewsService,
    private router: Router) {

  }

  ngOnInit() {
    this.setDocId();
  }

  setDocId() {
    this.reviewForm.controls.doctorId.setValue(this.activatedRoute.snapshot.queryParamMap.get('docId'));
  }

  onRatingSet(e: any) {
    this.reviewForm.controls.stars.setValue(e);
  }

  add() {
    const review = this.reviewForm.value;
    this.reviewsService.create(review).subscribe(res => {
      this.router.navigate(['/account/', review.doctorId], { fragment: '1' });
    });
  }

}
