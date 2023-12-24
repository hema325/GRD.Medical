import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-image-card',
  templateUrl: './image-card.component.html',
  styleUrls: ['./image-card.component.css']
})
export class ImageCardComponent {

  @Input() image: File | null = null;
  @Input() hideActions: boolean = false;
  @Output() imageRemoved = new EventEmitter();

  imageUrl: string = '';

  ngOnChanges() {
    if (this.image) {
      URL.revokeObjectURL(this.imageUrl);
      this.imageUrl = URL.createObjectURL(this.image);
    }
  }

  removeImage() {
    if (this.imageUrl)
      URL.revokeObjectURL(this.imageUrl);
    this.imageRemoved.emit();
  }

  ngOnDestory() {
    if (this.imageUrl)
      URL.revokeObjectURL(this.imageUrl);
  }

}
