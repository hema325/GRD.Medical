import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-audio-card',
  templateUrl: './audio-card.component.html',
  styleUrls: ['./audio-card.component.css']
})
export class AudioCardComponent {
  @Input() audio: File | null = null;
  player = new Audio();

  ngOnInit() {
    this.player.loop = true;
  }

  ngOnChanges() {
    if (this.audio)
      this.player.src = URL.createObjectURL(this.audio);
  }

  playPause() {
    if (this.player.paused)
      this.player.play();
    else
      this.player.pause();
  }

  ngOnDestroy() {
    this.player.pause();
    URL.revokeObjectURL(this.player.src);
  }
}
