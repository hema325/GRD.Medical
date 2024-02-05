import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-writing-loader',
  templateUrl: './writing-loader.component.html',
  styleUrls: ['./writing-loader.component.css']
})
export class WritingLoaderComponent {
  @Input() imageUrl = '';
}
