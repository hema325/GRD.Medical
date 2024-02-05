import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Appointment } from 'src/app/models/appointments/appointment';
import { AppointmentsService } from 'src/app/services/appointments.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-appointment-count-down',
  templateUrl: './appointment-count-down.component.html',
  styleUrls: ['./appointment-count-down.component.css']
})
export class AppointmentDetailsComponent {

  defaultUserImageUrl = environment.defaultUserImageUrl;
  appointment?: Appointment;

  days = '00';
  hours = '00';
  minutes = '00';
  seconds = '00';

  intervalId: any = null;

  constructor(private appointmentsService: AppointmentsService,
    private router: Router,
    private activatedRoute: ActivatedRoute) {
    const state = router.getCurrentNavigation()?.extras.state as Appointment;

    if (state)
      this.appointment = state;
  }

  ngOnInit() {
    if (!this.appointment)
      this.loadAppointment();
    else
      this.handleCountDown();
  }

  loadAppointment() {
    const id = Number(this.activatedRoute.snapshot.queryParamMap.get('appontId'));
    this.appointmentsService.getById(id).subscribe(res => {
      this.appointment = res;
      this.handleCountDown();
    });
  }

  isChatAvailable() {
    if (!this.appointment)
      return false;

    return new Date(this.appointment!.startDateTime).getTime() - new Date().getTime() <= 0;
  }

  handleCountDown() {
    if (!this.appointment)
      return;

    const target = new Date(this.appointment.startDateTime).getTime();
    this.calculateRemaining(target);

    this.intervalId = setInterval(() => this.calculateRemaining(target), 1000);
  }

  calculateRemaining(target: any) {

    const current = new Date().getTime();
    const span = target - current;

    if (span < 0) {
      clearInterval(this.intervalId);
      return;
    }
    const seconds = Math.floor((span / 1000) % 60);
    const minutes = Math.floor((span / 1000 / 60) % 60);
    const hours = Math.floor((span / 1000 / 60 / 60) % 24);
    const days = Math.floor((span / 1000 / 60 / 60 / 24));

    this.seconds = seconds < 10 ? '0' + seconds : seconds.toString();
    this.minutes = minutes < 10 ? '0' + minutes : minutes.toString();
    this.hours = hours < 10 ? '0' + hours : hours.toString();
    this.days = days < 10 ? '0' + days : days.toString();
  }

  goToChat() {
    this.router.navigate(['/appointments/chat'], { state: this.appointment, queryParams: { appontId: this.appointment?.id } })
  }

  ngOnDestroy() {
    clearInterval(this.intervalId);
  }

}
