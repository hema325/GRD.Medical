import { Component, Inject } from '@angular/core';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { CommentsService } from 'src/app/services/comments.service';

@Component({
  selector: 'app-edit-comment-bottom-sheet',
  templateUrl: './edit-comment-bottom-sheet.component.html',
  styleUrls: ['./edit-comment-bottom-sheet.component.css']
})
export class EditCommentBottomSheetComponent {

  constructor(@Inject(MAT_BOTTOM_SHEET_DATA) private commentId: number,
    private bottomSheetRef: MatBottomSheetRef,
    private commentsSerivce: CommentsService) { }

  removeComment() {
    this.commentsSerivce.delete(this.commentId).subscribe({
      next: () => this.bottomSheetRef.dismiss({ commentDeleted: true }),
      error: () => this.bottomSheetRef.dismiss({ commentDeleted: false })
    })
  }
}
