import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-welcoming-view',
  templateUrl: './welcoming-view.component.html',
  styleUrls: ['./welcoming-view.component.css']
})
export class WelcomingViewComponent {
  suggestions: string[] =
    [
      "Can you describe your usual sleep pattern and duration?",
      "What is your average daily physical activity level?",
      "How often do you experience digestive issues or discomfort?",
      "How often do you engage in aerobic exercise each week?"
    ];

  @Output() suggestionChoosed = new EventEmitter<string>();

  suggest(msg: string) {
    this.suggestionChoosed.emit(msg);
  }
}
