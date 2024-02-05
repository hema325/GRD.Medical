import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { AuthResult } from 'src/app/models/account/auth-result';
import { CreateComment } from 'src/app/models/comments/create-comment';
import { AccountService } from 'src/app/services/account.service';
import { CommentsService } from 'src/app/services/comments.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.css']
})
export class CreateCommentComponent {

  defaultUserImageUrl = environment.defaultUserImageUrl;
  currentAuth: AuthResult | null = null;
  isEmojiListActive = false;

  createCommentObj: CreateComment = {
    content: '',
    image: null,
    replyTo: null,
    postId: 0
  }

  constructor(private accountService: AccountService,
    private commentsService: CommentsService,
    private router: Router) { }

  ngOnInit() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

  isCommentValid() {
    return this.createCommentObj.content || this.createCommentObj.image;
  }

  @ViewChild('commentInput') commentInput?: ElementRef;

  setImages(event: any) {
    if (!event.target)
      return;

    this.createCommentObj.image = event.target.files[0];
    this.commentInput?.nativeElement.focus();
  }

  removeImage() {
    this.createCommentObj.image = null;
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

    if (!this.currentAuth) {
      this.router.navigate(['/account/login'], { queryParams: { returnUrl: '/posts/' + this.createCommentObj.postId } });
      return;
    }

    this.commentsService.create(this.createCommentObj)
      .subscribe(() => this.resetTextArea());
  }

  resetTextArea() {
    this.createCommentObj.image = null;
    const ele = this.commentInput?.nativeElement;
    ele.value = '';
    ele.dispatchEvent(new Event('input'));
  }
}

