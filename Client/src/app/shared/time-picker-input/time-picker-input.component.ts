import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-time-picker-input',
  templateUrl: './time-picker-input.component.html',
  styleUrls: ['./time-picker-input.component.css']
})
export class TimePickerInputComponent {
  @Input() format = 12;
  @Input() label = '';
  @Input() control!: FormControl;
  @Input() disabled = false;
}
