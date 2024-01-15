import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Post } from 'src/app/models/posts/post';
import { environment } from 'src/environments/environment.development';
import { Media } from 'src/app/models/media';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { EditPostBottomSheetComponent } from '../edit-post-bottom-sheet/edit-post-bottom-sheet.component';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
})
export class PostCardComponent {

  defaultUserImageUrl = environment.defaultUserImageUrl;

  @Input() post: Post | null = null;
  @Input() amIPostOwner: boolean = false;
  @Output() bottomSheetBtnClicked = new EventEmitter();
  @Input() commentsActive: boolean = false;

  constructor(private postBottomSheet: MatBottomSheet) { }

  openPostBottomSheet(postId: number) {
    const bottomSheet = this.postBottomSheet.open(EditPostBottomSheetComponent, { data: postId });
    bottomSheet.afterDismissed().subscribe(res => {
      if (res.postDeleted)
        this.bottomSheetBtnClicked.emit();
    });
  }

  getMediaUrls(medias: Media[]) {
    return medias.map(m => m.url);
  }

}
