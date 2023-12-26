import { Directive, ElementRef, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { Subject, Subscription, take, takeUntil, takeWhile, timer } from 'rxjs';

@Directive({
  selector: '[longPress]'
})
export class LongPressDirective {

  @Input() duration: number = 500;
  @Output() longPress = new EventEmitter();

  constructor(private elementRef: ElementRef) { }


  mouseUp = new Subject<void>();
  timerSubscription: Subscription | undefined;

  ngOnInit() {
    this.mouseUp.subscribe(() => {
      if (this.timerSubscription)
        this.timerSubscription.unsubscribe();
    });

    this.elementRef.nativeElement.classList.add('long-press');
  }

  @HostListener('mousedown')
  @HostListener('touchstart')
  onMouseDown() {
    this.timerSubscription = timer(this.duration)
      .pipe(takeUntil(this.mouseUp))
      .subscribe(() => this.longPress.emit());
  }

  @HostListener('mouseup')
  @HostListener('touchend')
  onMouseUp() {
    this.mouseUp.next();
  }

  ngOnDestroy() {
    this.mouseUp.next(); // unsubscribe the existing subscription
    this.mouseUp.complete();
  }

}
