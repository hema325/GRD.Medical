import { Component, ElementRef, ViewChild } from '@angular/core';
import { CreateComment } from 'src/app/models/CreateComment';

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.css']
})
export class CreateCommentComponent {
  createComment: CreateComment = {
    content: '',
    image: null,
    imageUrl: null
  }

  setImages(event: any) {
    if (!event.target)
      return;

    this.createComment.image = event.target.files[0];
    this.createComment.imageUrl = URL.createObjectURL(event.target.files[0]);
  }

  @ViewChild('commentInput') commentInput?: ElementRef;

  removeImage() {
    this.createComment.image = null;
    if (this.createComment.imageUrl)
      URL.revokeObjectURL(this.createComment.imageUrl);

    console.log(this.commentInput);

    if (!this.commentInput)
      return;

    if (!this.createComment.content && !this.createComment.image)
      this.commentInput.nativeElement.removeAttribute('style');
  }

  textareaHeight: number = 100;

  extandTextArea(event: any) {
    if (!event.target)
      return;

    event.target.style.height = this.textareaHeight + 'px'
  }

  autoResizeTextArea(event: any) {
    if (event.target.scrollHeight >= this.textareaHeight)
      event.target.style.height = event.target.scrollHeight + 'px';
  }

  minimizeTextArea(event: any) {
    if (!event.target)
      return;

    if (!this.createComment.content && !this.createComment.image)
      event.target.removeAttribute('style');
  }
}

