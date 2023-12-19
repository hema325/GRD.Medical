import { Component } from '@angular/core';
import { CreatePost } from 'src/app/models/CreatePost';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent {

  createPost: CreatePost = {
    content: '',
    images: null,
    imagesUrl: null
  };

  isPostFormValid() {
    return this.createPost.content != '' || Number(this.createPost.images?.length) > 0;
  }

  setImages(e: any) {
    if (this.createPost.imagesUrl)
      this.createPost.imagesUrl.forEach(url => URL.revokeObjectURL(url));

    this.createPost.images = [...e.target?.files];
    this.createPost.imagesUrl = this.createPost.images?.map(img => URL.createObjectURL(img)) ?? null;
  }


  removeImage(index: number) {
    if (!this.createPost.images || !this.createPost.imagesUrl)
      return;

    URL.revokeObjectURL(this.createPost.imagesUrl[index]);
    this.createPost.images.splice(index, 1);
    this.createPost.imagesUrl.splice(index, 1);
  }

  ngOnDestroy(): void {
    if (this.createPost.imagesUrl)
      this.createPost.imagesUrl.forEach(url => URL.revokeObjectURL(url));
  }

}
