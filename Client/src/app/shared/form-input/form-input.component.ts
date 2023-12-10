import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-form-input',
  templateUrl: './form-input.component.html',
  styleUrls: ['./form-input.component.css']
})
export class FormInputComponent {
  @Input() type = 'text';
  @Input() label = '';
  @Input() control!: FormControl;
  @Input() disabled = false;
}
