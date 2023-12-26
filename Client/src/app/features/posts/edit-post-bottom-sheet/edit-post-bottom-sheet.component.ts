import { Component, Inject } from '@angular/core';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { take } from 'rxjs';
import { PostsService } from 'src/app/services/posts.service';

@Component({
  selector: 'app-edit-post-bottom-sheet',
  templateUrl: './edit-post-bottom-sheet.component.html',
  styleUrls: ['./edit-post-bottom-sheet.component.css']
})
export class EditPostBottomSheetComponent {


  constructor(@Inject(MAT_BOTTOM_SHEET_DATA) private postId: number,
    private bottomSheetRef: MatBottomSheetRef,
    private postsService: PostsService) { }

  removePost() {
    this.postsService.delete(this.postId).pipe(take(1)).subscribe({
      next: () => this.bottomSheetRef.dismiss({ postDeleted: true }),
      error: () => this.bottomSheetRef.dismiss({ postDeleted: false })
    });
  }

}
