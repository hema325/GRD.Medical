import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-chat-timer',
  templateUrl: './chat-timer.component.html',
  styleUrls: ['./chat-timer.component.css']
})
export class ChatTimerComponent {

  @Input() endTime: Date | string | undefined = undefined;
  @Output() timeout = new EventEmitter();

  hours = '';
  minutes = '00';
  seconds = '00';
  intervalId: any = null;

  timerEnded = false;

  ngOnInit() {
    this.handleCountDown();
  }

  handleCountDown() {
    if (!this.endTime)
      return;

    const target = new Date(this.endTime).getTime();
    this.calculateRemaining(target);

    this.intervalId = setInterval(() => this.calculateRemaining(target), 1000);
  }

  calculateRemaining(target: any) {

    const current = new Date().getTime();
    const span = target - current;

    if (span < 0) {
      this.timerEnded = true;
      clearInterval(this.intervalId);
      this.timeout.emit();
      return;
    }
    const seconds = Math.floor((span / 1000) % 60);
    const minutes = Math.floor((span / 1000 / 60) % 60);
    const hours = Math.floor(span / 1000 / 60 / 60);

    this.seconds = seconds < 10 ? '0' + seconds : seconds.toString();
    this.minutes = minutes < 10 ? '0' + minutes : minutes.toString();
    if (hours > 0)
      this.hours = hours < 10 ? '0' + hours : hours.toString();
  }
}
