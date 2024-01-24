import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-text-area-input',
  templateUrl: './text-area-input.component.html',
  styleUrls: ['./text-area-input.component.css']
})
export class TextAreaInputComponent {
  @Input() label = '';
  @Input() control!: FormControl;
  @Input() disabled = false;
}
