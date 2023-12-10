import { Component } from '@angular/core';
import { AiModelsResponse } from 'src/app/models/ai-models-response';

@Component({
  selector: 'app-skin-checking',
  templateUrl: './skin-checking.component.html',
  styleUrls: ['./skin-checking.component.css']
})
export class SkinCheckingComponent {

  image: any = null;
  response: AiModelsResponse | null = null;
  src: string | null = null;

  setSrc() {
    if (this.src)
      URL.revokeObjectURL(this.src);

    this.src = URL.createObjectURL(this.image);
  }

  ngOnDestory() {
    if (this.src)
      URL.revokeObjectURL(this.src);
  }

}
