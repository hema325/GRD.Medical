import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-message-card',
  templateUrl: './message-card.component.html',
  styleUrls: ['./message-card.component.css']
})
export class MessageCardComponent {

  @Input() msg?: {
    imageUrl: string
    content?: string
    messagedOn: Date | string | null
    isMain: boolean
    imageUrls?: string[]
  };

}
