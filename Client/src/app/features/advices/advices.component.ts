import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-advices',
  templateUrl: './advices.component.html',
  styleUrls: ['./advices.component.css']
})
export class AdvicesComponent {
  date = new Date();

  filterForm = this.fb.group({
    title: ['', Validators.required]
  })

  constructor(private fb: FormBuilder) { }

  handlePageEvent(event: any) {
    console.log(event);
  }
}
