import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-date-picker-input',
  templateUrl: './date-picker-input.component.html',
  styleUrls: ['./date-picker-input.component.css']
})
export class DatePickerInputComponent {
  @Input() label = '';
  @Input() control!: FormControl;
  @Input() disabled = false;
  @Input() min: Date | null = null;
  @Output() changed = new EventEmitter();

  change() {
    this.changed.emit();
  }
}
