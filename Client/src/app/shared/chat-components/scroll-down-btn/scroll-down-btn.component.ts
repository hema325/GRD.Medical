import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-scroll-down-btn',
  templateUrl: './scroll-down-btn.component.html',
  styleUrls: ['./scroll-down-btn.component.css']
})
export class ScrollDownBtnComponent {
  @Input() active = false;
  @Output() clicked = new EventEmitter();

  click() {
    this.clicked.emit();
  }
}
