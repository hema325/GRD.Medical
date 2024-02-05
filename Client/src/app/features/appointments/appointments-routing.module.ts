import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CreateAppointmentComponent } from './create-appointment/create-appointment.component';
import { AppointmentSuccessPageComponent } from './appointment-success-page/appointment-success-page.component';
import { AppointmentDetailsComponent } from './appointment-count-down/appointment-count-down.component';
import { AppointmentChatComponent } from './appointment-chat/appointment-chat.component';

const routes = [
  { path: 'create-appointment', component: CreateAppointmentComponent },
  { path: 'success', component: AppointmentSuccessPageComponent },
  { path: 'chat-count-down', component: AppointmentDetailsComponent },
  { path: 'chat', component: AppointmentChatComponent },
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppointmentsRoutingModule { }
