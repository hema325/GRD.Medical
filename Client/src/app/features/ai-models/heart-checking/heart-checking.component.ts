import { Component } from '@angular/core';
import { take } from 'rxjs';
import { AiModelsResponse } from 'src/app/models/ai-models-response';
import { AiModelsService } from 'src/app/services/ai-models.service';

@Component({
  selector: 'app-heart-checking',
  templateUrl: './heart-checking.component.html',
  styleUrls: ['./heart-checking.component.css']
})
export class HeartCheckingComponent {

  voice: any;
  prediction: AiModelsResponse | null = null;

  constructor(private AIModelsService: AiModelsService) { }

  check(event: any) {

    if (event instanceof FileList) {
      if (event[0].type.includes('audio'))
        this.voice = event[0];
      else
        return;
    }
    else
      this.voice = event.target?.files[0];

    if (this.voice) {
      this.AIModelsService.checkHeart(this.voice)
        .pipe(take(1)).subscribe(res => this.prediction = res);
    }
  }
}
