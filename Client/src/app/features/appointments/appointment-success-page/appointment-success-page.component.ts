import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-appointment-success-page',
  templateUrl: './appointment-success-page.component.html',
  styleUrls: ['./appointment-success-page.component.css']
})
export class AppointmentSuccessPageComponent {

  appointmentId: number = 0;

  constructor(private router: Router) {
    const state = this.router.getCurrentNavigation()?.extras.state as any;
    if (state?.appointmentId)
      this.appointmentId = state.appointmentId;
  }

}
