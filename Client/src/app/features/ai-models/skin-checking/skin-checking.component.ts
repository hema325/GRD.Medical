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

}
