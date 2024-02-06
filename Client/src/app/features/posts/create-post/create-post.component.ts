import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { PostsService } from 'src/app/services/posts.service';
import { CreatePost } from 'src/app/models/posts/create-post';
import { AccountService } from 'src/app/services/account.service';
import { AuthResult } from 'src/app/models/account/auth-result';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent {

  currentAuth: AuthResult | null = null;

  createPostObj: CreatePost = {
    content: '',
    images: null,
  };

  constructor(private postsService: PostsService,
    private accountService: AccountService,
    private router: Router) { }

  ngOnInit() {
    this.loadCurrentAuth();
  }


  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

  createPost() {

    if (!this.currentAuth) {
      this.router.navigate(['/account/login'], { queryParams: { returnUrl: '/posts' } });
      return;
    }

    this.createPostObj.content = this.createPostObj.content.trim();
    this.postsService.create(this.createPostObj)
      .subscribe(post => {
        this.createPostObj = {
          content: '',
          images: null,
        };
      });
  }

  isPostFormValid() {
    return this.createPostObj.content != '' || Number(this.createPostObj.images?.length) > 0;
  }

  setImages(e: any) {
    this.createPostObj.images = [...e.target?.files];
    this.postTextarea?.nativeElement.focus();
  }

  removeImage(index: number) {
    if (this.createPostObj.images)
      this.createPostObj.images.splice(index, 1);
  }

  @ViewChild('postTextarea') postTextarea?: ElementRef;

  insertText(text: string) {
    this.createPostObj.content = this.createPostObj.content + text;

    if (!this.postTextarea)
      return;

    const ele = this.postTextarea.nativeElement;
    const selectionStart = ele.selectionStart;
    const selectionEnd = ele.selectionEnd;

    const textToInsert = text;
    const currentText = ele.value;

    const newText = currentText.substring(0, selectionStart)
      + textToInsert
      + currentText.substring(selectionEnd);

    this.createPostObj.content = newText;

    const newCursorPosition = selectionStart + currentText.length;
    ele.focus();
    ele.setSelectionRange(newCursorPosition, newCursorPosition);
  }

}
