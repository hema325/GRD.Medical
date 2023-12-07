import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})
export class ArticlesComponent {
  date = new Date();

  filterForm = this.fb.group({
    title: ['', Validators.required]
  })

  constructor(private fb: FormBuilder) { }

  handlePageEvent(event: any) {
    console.log(event);
  }
}
