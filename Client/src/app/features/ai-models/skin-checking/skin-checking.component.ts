import { Component } from '@angular/core';
import { AiModelsResponse } from 'src/app/models/ai-models-response';

@Component({
  selector: 'app-skin-checking',
  templateUrl: './skin-checking.component.html',
  styleUrls: ['./skin-checking.component.css']
})
export class SkinCheckingComponent {

  image: any = null;
  imageUrl: string | null = null;
  response: AiModelsResponse | null = null;


  setSrc() {
    if (this.imageUrl)
      URL.revokeObjectURL(this.imageUrl);

    this.imageUrl = URL.createObjectURL(this.image);
  }

  ngOnDestory() {
    if (this.imageUrl)
      URL.revokeObjectURL(this.imageUrl);
  }

}
