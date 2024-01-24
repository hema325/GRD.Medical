import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-experience-select-input',
  templateUrl: './experience-select-input.component.html',
  styleUrls: ['./experience-select-input.component.css']
})
export class ExperienceSelectInputComponent {
  @Input() control!: FormControl;
  @Input() label: string = '';
  @Input() max: number = 0;
  @Input() disableAny = false;
  @Input() default?: string;

  options: any[] = [];

  ngOnInit() {
    this.initExperienceOptions();
  }

  initExperienceOptions() {
    for (let cnt = 1; cnt <= this.max; ++cnt)
      this.options.push({ value: cnt.toString(), text: cnt });
  }
}
