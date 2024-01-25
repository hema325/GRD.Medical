import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { TimeSlotFilter } from '../models/time-slots/time-slot-filter';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginatedList } from '../models/paginated-list';
import { TimeSlot } from '../models/time-slots/time-slot';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TimeSlotsService {

  baseUrl = environment.baseUrl + '/timeSlots';

  private createdTimeSlot = new BehaviorSubject<TimeSlot | null>(null);
  createdTimeSlot$ = this.createdTimeSlot.asObservable();

  constructor(private httpClient: HttpClient) { }

  create(timeSlot: any) {
    return this.httpClient.post<TimeSlot>(this.baseUrl, timeSlot).pipe(map(res => {
      this.createdTimeSlot.next(res);
      return res;
    }));
  }

  delete(id: number) {
    return this.httpClient.delete(this.baseUrl + '/' + id);
  }

  get(filter: TimeSlotFilter) {
    let params = new HttpParams();
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<TimeSlot>>(this.baseUrl, { params });
  }
}
