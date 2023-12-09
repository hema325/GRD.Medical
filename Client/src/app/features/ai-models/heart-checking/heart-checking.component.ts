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
  player = new Audio();
  response: AiModelsResponse | null = null;

  constructor(private AIModelsService: AiModelsService) { }

  playPause() {

    if (this.player.paused)
      this.player.play();
    else
      this.player.pause();

  }

  setPlayer() {
    URL.revokeObjectURL(this.player.src);
    this.player.src = URL.createObjectURL(this.voice);
    this.player.loop = true;
    if (this.voice) {
      const fd = new FormData();
      fd.append('voice', this.voice);
      this.AIModelsService.checkHeart(fd).pipe(take(1)).subscribe(res => this.response = res);
    }
  }

  ngOnDestroy() {
    this.player.pause();
    URL.revokeObjectURL(this.player.src);
  }
}
