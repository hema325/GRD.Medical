import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { PaginatedList } from 'src/app/models/paginated-list';
import { Post } from 'src/app/models/posts/post';
import { PostFilter } from 'src/app/models/post-filter';
import { PostsService } from 'src/app/services/posts.service';
import { CreatePost } from 'src/app/models/posts/create-post';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent {

  createPostObj: CreatePost = {
    content: '',
    images: null,
  };

  postFilter: PostFilter = {
    ownerId: null,
    pageNumber: 1,
    pageSize: 6
  }

  constructor(private postsService: PostsService) { }

  createPost() {
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
