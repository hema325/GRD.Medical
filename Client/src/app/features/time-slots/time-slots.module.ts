import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeSlotsListComponent } from './time-slots-list/time-slots-list.component';
import { CreateTimeSlotComponent } from './create-time-slot/create-time-slot.component';
import { SharedModule } from 'src/app/shared/shared.module';



@NgModule({
  declarations: [
    TimeSlotsListComponent,
    CreateTimeSlotComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    TimeSlotsListComponent,
    CreateTimeSlotComponent
  ]
})
export class TimeSlotsModule { }
