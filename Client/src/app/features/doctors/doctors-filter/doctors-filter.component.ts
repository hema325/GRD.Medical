import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { DoctorFilter } from 'src/app/models/users/doctor-filter';

@Component({
  selector: 'app-doctors-filter',
  templateUrl: './doctors-filter.component.html',
  styleUrls: ['./doctors-filter.component.css']
})
export class DoctorsFilterComponent {

  @Input() seed: any = null;

  filterForm = this.fb.group({
    specialityId: ['default'],
    name: [''],
    orderBy: ['default'],
    experience: ['default']
  })

  @Output() filterRequested = new EventEmitter<any>();

  orderByOptions = [
    { value: 'feeAsc', text: 'Fee Ascending' },
    { value: 'feeDesc', text: 'Fee Descending' },
    { value: 'experAsc', text: 'Experience Ascending' },
    { value: 'experDesc', text: 'Experience Descending' },
  ]

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.filterForm.patchValue(this.seed);
  }



  applyFilter() {
    const filter = this.filterForm.value;
    this.filterRequested.emit({
      name: filter.name,
      specialityId: filter.specialityId == 'default' ? null : Number(filter.specialityId),
      orderBy: filter.orderBy == 'default' ? null : filter.orderBy,
      experience: filter.experience == 'default' ? null : Number(filter.experience),
    });
  }


}
