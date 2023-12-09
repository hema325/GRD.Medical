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
<<<<<<< HEAD
=======
  @Input() disabled = false;
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf
}
