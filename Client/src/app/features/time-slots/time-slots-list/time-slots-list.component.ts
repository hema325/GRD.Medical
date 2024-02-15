import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { PaginatedList } from 'src/app/models/paginated-list';
import { TimeSlot } from 'src/app/models/time-slots/time-slot';
import { TimeSlotFilter } from 'src/app/models/time-slots/time-slot-filter';
import { TimeSlotsService } from 'src/app/services/time-slots.service';

@Component({
  selector: 'app-time-slots-list',
  templateUrl: './time-slots-list.component.html',
  styleUrls: ['./time-slots-list.component.css']
})
export class TimeSlotsListComponent {
  displayedColumns: string[] = ['day', 'start', 'end', 'id'];

  timeSlots: PaginatedList<TimeSlot> | null = null;
  timeSlotFilter: TimeSlotFilter = {
    pageNumber: 1,
    pageSize: 10
  }
  isLoading = false;

  constructor(private timeSlotsService: TimeSlotsService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.loadTimeSlots();
    this.addCreatedTimeSlots();
  }

  loadTimeSlots() {
    this.isLoading = true;
    this.timeSlotsService.get(this.timeSlotFilter)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe(res => {
        this.timeSlots = res;
      });
  }

  addCreatedTimeSlots() {
    this.timeSlotsService.createdTimeSlot$.subscribe(res => {
      if (res && this.timeSlots) {
        this.timeSlots.data = [res, ...this.timeSlots.data]
        this.timeSlots.totalCount += 1;
      }
    })
  }

  handlePageEvent(event: PageEvent) {
    this.timeSlotFilter.pageSize = event.pageSize;
    this.timeSlotFilter.pageNumber = event.pageIndex + 1;
    this.loadTimeSlots();
  }

  deleteTimeSlot(id: any) {
    this.timeSlotsService.delete(id).subscribe(res => {
      this.toastr.success('Time slot deleted successfully.');
      if (this.timeSlots) {
        const data = this.timeSlots.data;
        this.timeSlots.data.splice(data.findIndex(ts => ts.id == id), 1);
        this.timeSlots.data = [...data];
        this.timeSlots.totalCount -= 1;
      }
    })
  }
}

