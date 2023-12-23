import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { PaginatedList } from 'src/app/models/paginated-list';
import { Post } from 'src/app/models/posts/post';
import { PostFilter } from 'src/app/models/post-filter';
import { PostsService } from 'src/app/services/posts.service';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent {

  createPostObj: {
    content: string;
    images: File[] | null,
    imagesUrl: string[] | null
  } = {
      content: '',
      images: null,
      imagesUrl: null
    };

  postFilter: PostFilter = {
    ownerId: null,
    pageNumber: 1,
    pageSize: 6
  }

  constructor(private postsService: PostsService) { }

  createPost() {
    this.postsService.create({
      content: this.createPostObj.content,
      images: this.createPostObj.images
    }).subscribe(post => {
      this.createPostObj = {
        content: '',
        images: null,
        imagesUrl: null
      };
    });
  }


  isPostFormValid() {
    return this.createPostObj.content != '' || Number(this.createPostObj.images?.length) > 0;
  }

  setImages(e: any) {
    if (this.createPostObj.imagesUrl)
      this.createPostObj.imagesUrl.forEach(url => URL.revokeObjectURL(url));

    this.createPostObj.images = [...e.target?.files];
    this.createPostObj.imagesUrl = this.createPostObj.images?.map(img => URL.createObjectURL(img)) ?? null;
  }


  removeImage(index: number) {
    if (!this.createPostObj.images || !this.createPostObj.imagesUrl)
      return;

    URL.revokeObjectURL(this.createPostObj.imagesUrl[index]);
    this.createPostObj.images.splice(index, 1);
    this.createPostObj.imagesUrl.splice(index, 1);
  }

  ngOnDestroy(): void {
    if (this.createPostObj.imagesUrl)
      this.createPostObj.imagesUrl.forEach(url => URL.revokeObjectURL(url));
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
