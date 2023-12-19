import { Component, ElementRef, ViewChild } from '@angular/core';
import { CreateComment } from 'src/app/models/CreateComment';

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.css']
})
export class CreateCommentComponent {

  isEmojiListActive = false;

  createComment: CreateComment = {
    content: '',
    image: null,
    imageUrl: null
  }

  isCommentValid() {
    return this.createComment.content || this.createComment.image;
  }

  setImages(event: any) {
    if (!event.target)
      return;

    this.createComment.image = event.target.files[0];
    this.createComment.imageUrl = URL.createObjectURL(event.target.files[0]);

    this.commentInput?.nativeElement.focus();
  }

  @ViewChild('commentInput') commentInput?: ElementRef;

  removeImage() {
    this.createComment.image = null;
    if (this.createComment.imageUrl)
      URL.revokeObjectURL(this.createComment.imageUrl);

    if (!this.commentInput)
      return;

    if (!this.createComment.content && !this.createComment.image)
      this.commentInput.nativeElement.removeAttribute('style');
  }

  textareaHeight: number = 100;

  extandTextArea() {
    if (!this.commentInput)
      return;

    this.commentInput.nativeElement.style.height = this.textareaHeight + 'px'
  }

  autoResizeTextArea() {
    if (!this.commentInput)
      return;

    const ele = this.commentInput.nativeElement;

    if (ele.scrollHeight >= this.textareaHeight)
      ele.style.height = ele.scrollHeight + 'px';
  }

  minimizeTextArea() {
    if (!this.commentInput)
      return;

    if (!this.createComment.content && !this.createComment.image && !this.isEmojiListActive)
      this.commentInput.nativeElement.removeAttribute('style');
  }

  insertText(text: string) {
    this.createComment.content = this.createComment.content + text;

    if (!this.commentInput)
      return;

    const ele = this.commentInput.nativeElement;
    const selectionStart = ele.selectionStart;
    const selectionEnd = ele.selectionEnd;

    const textToInsert = text;
    const currentText = ele.value;

    const newText = currentText.substring(0, selectionStart)
      + textToInsert
      + currentText.substring(selectionEnd);

    this.createComment.content = newText;

    const newCursorPosition = selectionStart + currentText.length;
    ele.setSelectionRange(newCursorPosition, newCursorPosition);
    this.commentInput?.nativeElement.focus();
  }

  comment() {
    console.log(this.createComment);
  }
}

