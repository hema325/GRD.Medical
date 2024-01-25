import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { TimeSlotsService } from 'src/app/services/time-slots.service';

@Component({
  selector: 'app-create-time-slot',
  templateUrl: './create-time-slot.component.html',
  styleUrls: ['./create-time-slot.component.css']
})
export class CreateTimeSlotComponent {
  selectedTime: any = null;

  timeSlotForm = this.fb.group({
    start: ['', [Validators.required]],
    end: ['', [Validators.required]],
    day: ['', [Validators.required]],
  })

  dayOptions = [
    { value: 'Sunday', text: 'Sunday' },
    { value: 'Monday', text: 'Monday' },
    { value: 'Tuesday', text: 'Tuesday' },
    { value: 'Wednesday', text: 'Wednesday' },
    { value: 'Thursday', text: 'Thursday' },
    { value: 'Friday', text: 'Friday' },
    { value: 'Saturday', text: 'Saturday' }
  ];

  constructor(private fb: FormBuilder,
    private timeSlotsService: TimeSlotsService,
    private toastrService: ToastrService) {

  }

  Create() {
    this.timeSlotsService.create(this.timeSlotForm.value)
      .subscribe(res => this.toastrService.success('Time slot is added successfully.'));
  }
}
