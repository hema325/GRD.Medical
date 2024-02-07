import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { finalize, take } from 'rxjs';
import { Roles } from 'src/app/models/account/roles.enum';
import { Appointment } from 'src/app/models/appointments/appointment';
import { AppointmentStatuses } from 'src/app/models/appointments/appointment-statuses.enum';
import { FilterBase } from 'src/app/models/filter-base';
import { PaginatedList } from 'src/app/models/paginated-list';
import { AccountService } from 'src/app/services/account.service';
import { AppointmentsService } from 'src/app/services/appointments.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-appointments-list',
  templateUrl: './appointments-list.component.html',
  styleUrls: ['./appointments-list.component.css']
})
export class AppointmentsListComponent {
  defaultUserImageUrl = environment.defaultUserImageUrl;
  displayedColumns: string[] = ['date', 'startTime', 'endTime', '', 'fee', 'status', 'id'];
  appointments: PaginatedList<Appointment> | null = null;

  appointmentFilter: FilterBase = {
    pageNumber: 1,
    pageSize: 10
  }
  isLoading = false;
  constructor(private appointmentsService: AppointmentsService,
    private router: Router,
    private accountService: AccountService) { }

  ngOnInit() {
    this.loadAppointments();
    this.handleDisplaedColumns();
  }

  handleDisplaedColumns() {
    this.accountService.currentAuth$.pipe(take(1)).subscribe(auth => {
      if (auth?.role == Roles.Patient || auth?.role == Roles.Admin)
        this.displayedColumns[3] = 'doctor';
      else if (auth?.role == Roles.Doctor)
        this.displayedColumns[3] = 'patient';
    })
  }

  loadAppointments() {
    this.isLoading = true;
    this.appointmentsService.get(this.appointmentFilter)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe(res => {
        this.appointments = res;
      });
  }

  handlePageEvent(event: PageEvent) {
    this.appointmentFilter.pageSize = event.pageSize;
    this.appointmentFilter.pageNumber = event.pageIndex + 1;
    this.loadAppointments();
  }

  getStatusClasses(status: string) {
    return { 'bg-success': status == AppointmentStatuses.Completed, 'bg-primary': status == AppointmentStatuses.Scheduled };
  }

  navigateToAppointment(appointment: Appointment) {
    let path = '/appointments/chat-count-down';
    if (new Date() >= new Date(appointment.startDateTime))
      path = '/appointments/chat';

    this.router.navigate([path], { state: appointment, queryParams: { appontId: appointment.id } })
  }

  isAppointmentEnded(appointment: Appointment) {
    return new Date() >= new Date(appointment.endDateTime) && appointment.status != AppointmentStatuses.Completed;
  }

  markCompleted(appointment: Appointment) {
    this.appointmentsService.markCompleted(appointment.id).subscribe(res => {
      appointment.status = AppointmentStatuses.Completed;
    });
  }

}
