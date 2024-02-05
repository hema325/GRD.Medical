import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateAppointmentComponent } from './create-appointment/create-appointment.component';
import { AppointmentsRoutingModule } from './appointments-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { AppointmentSuccessPageComponent } from './appointment-success-page/appointment-success-page.component';
import { AppointmentsListComponent } from './appointments-list/appointments-list.component';
import { AppointmentDetailsComponent } from './appointment-count-down/appointment-count-down.component';
import { AppointmentChatComponent } from './appointment-chat/appointment-chat.component';
import { ChatTimerComponent } from './appointment-chat/chat-timer/chat-timer.component';



@NgModule({
  declarations: [
    CreateAppointmentComponent,
    AppointmentSuccessPageComponent,
    AppointmentsListComponent,
    AppointmentDetailsComponent,
    AppointmentChatComponent,
    ChatTimerComponent
  ],
  imports: [
    CommonModule,
    AppointmentsRoutingModule,
    SharedModule
  ],
  exports: [
    AppointmentsListComponent
  ]
})
export class AppointmentsModule { }
