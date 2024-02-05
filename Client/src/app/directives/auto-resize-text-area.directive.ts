import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[AutoResizeTextArea]'
})
export class AutoResizeTextAreaDirective {

  @Input() defaultHeight: string = 'auto';

  constructor(private elementRef: ElementRef) { }

  @HostListener("input")
  input() {
    const ele = this.elementRef.nativeElement
    ele.style.height = this.defaultHeight;
    ele.style.height = ele.scrollHeight + 'px';
  }

  @HostListener("blur")
  blur() {
    const ele = this.elementRef.nativeElement;
    if (!ele.value)
      ele.style.height = this.defaultHeight;
  }
}
