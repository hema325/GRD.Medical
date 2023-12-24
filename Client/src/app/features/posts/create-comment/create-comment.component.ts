import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { take } from 'rxjs';
import { AuthResult } from 'src/app/models/account/auth-result';
import { CreateComment } from 'src/app/models/comments/create-comment';
import { AccountService } from 'src/app/services/account.service';
import { CommentsService } from 'src/app/services/comments.service';

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.css']
})
export class CreateCommentComponent {

  currentAuth: AuthResult | null = null;
  isEmojiListActive = false;

  createCommentObj: CreateComment = {
    content: '',
    image: null,
    replyTo: null,
    postId: 0
  }

  constructor(private accountService: AccountService,
    private commentsService: CommentsService) { }

  ngOnInit() {
    this.accountService.currentAuth$.pipe(take(1)).subscribe(auth => this.currentAuth = auth);
  }

  isCommentValid() {
    return this.createCommentObj.content || this.createCommentObj.image;
  }

  setImages(event: any) {
    if (!event.target)
      return;

    this.createCommentObj.image = event.target.files[0];
    this.commentInput?.nativeElement.focus();
  }

  @ViewChild('commentInput') commentInput?: ElementRef;

  removeImage() {
    this.createCommentObj.image = null;

    if (!this.commentInput)
      return;

    if (!this.createCommentObj.content && !this.createCommentObj.image)
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

    if (!this.createCommentObj.content && !this.createCommentObj.image && !this.isEmojiListActive)
      this.commentInput.nativeElement.removeAttribute('style');
  }

  insertText(text: string) {
    this.createCommentObj.content = this.createCommentObj.content + text;

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

    this.createCommentObj.content = newText;

    const newCursorPosition = selectionStart + currentText.length;
    ele.setSelectionRange(newCursorPosition, newCursorPosition);
    this.commentInput?.nativeElement.focus();
  }

  @Input() postId: number = 0;
  @Input() replyTo: number | null = null;

  ngOnChanges() {
    this.createCommentObj.postId = this.postId;
    this.createCommentObj.replyTo = this.replyTo;
  }

  createComment() {

    this.commentsService.create(this.createCommentObj)
      .subscribe(comment => this.resetTextArea());
  }

  resetTextArea() {
    this.createCommentObj.content = '';
    this.createCommentObj.image = null;

    this.isEmojiListActive = false;
    this.minimizeTextArea();
  }
}

