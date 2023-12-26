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


  check(event: any) {
    if (event instanceof FileList) {
      if (event[0].type.includes('image'))
        this.image = event[0];
      else
        return;
    }
    else
      this.image = event.target?.files[0];
  }

}
