import { Directive, ElementRef, EventEmitter, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[reachedBottom]'
})
export class ReachedBottomDirective {

  @Output() reachedBottom = new EventEmitter();

  constructor(private elementRef: ElementRef) { }

  @HostListener('window:scroll') onScroll() {
    const container = this.elementRef.nativeElement;
    const containerRect = container.getBoundingClientRect();
    const containerBottom = containerRect.bottom;
    const windowHeight = window.innerHeight || document.documentElement.clientHeight;

    if (containerBottom <= windowHeight)
      this.reachedBottom.emit();
  }
}
