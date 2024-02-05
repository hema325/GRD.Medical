import { Component, EventEmitter, Input, Output, Renderer2, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSelect } from '@angular/material/select';

@Component({
  selector: 'app-form-select',
  templateUrl: './form-select.component.html',
  styleUrls: ['./form-select.component.css']
})
export class FormSelectComponent {
  @Input() label = '';
  @Input() control!: FormControl;
  @Input() disabled = false;
  @Input() multiple = false;
  @Input() options?: {
    value: string | number
    text: string
  }[] = [];
  @Output() scrollAtBottom = new EventEmitter();
  @Input() loading = false;
  @Input() default?: string;
  constructor(private renderer: Renderer2) { }

  PIXEL_TOLERANCE = 3;
  @ViewChild('selectInput') selectInput!: MatSelect;

  ngAfterViewInit() {
    this.selectInput.openedChange.subscribe(() => this.registerScrollEvent());
  }

  registerScrollEvent() {
    const ele = this.selectInput.panel.nativeElement;
    this.renderer.listen(ele, 'scroll', e => this.onScroll(e))
  }

  onScroll(e: any) {
    const ele = e.target as HTMLElement;

    if (ele.scrollHeight - ele.clientHeight - ele.scrollTop < this.PIXEL_TOLERANCE)
      this.scrollAtBottom.emit();
  }

  compareSelection(first: any, second: any) {
    return first && second && first == second;
  }

}
