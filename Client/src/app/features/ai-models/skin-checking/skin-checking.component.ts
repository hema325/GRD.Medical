import { Component } from '@angular/core';
import { AiModelsResponse } from 'src/app/models/ai-models-response';
import { AiModelsService } from 'src/app/services/ai-models.service';

@Component({
  selector: 'app-skin-checking',
  templateUrl: './skin-checking.component.html',
  styleUrls: ['./skin-checking.component.css']
})
export class SkinCheckingComponent {

  image: any = null;
  prediction: AiModelsResponse | null = null;

  constructor(private aiModelsService: AiModelsService) { }


  check(event: any) {
    if (event instanceof FileList) {
      if (event[0].type.includes('image'))
        this.image = event[0];
      else
        return;
    }
    else
      this.image = event.target?.files[0];

    if (this.image) {
      this.aiModelsService.checkSkin(this.image).subscribe(res => this.prediction = res);
    }
  }

}
