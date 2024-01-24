import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { finalize, take } from 'rxjs';
import { PaginatedList } from 'src/app/models/paginated-list';
import { Speciality } from 'src/app/models/specialists/Speciality';
import { SpecialistFilter } from 'src/app/models/specialists/specialist-filter';
import { SpecialistsService } from 'src/app/services/specialists.service';

@Component({
  selector: 'app-specialities',
  templateUrl: './specialities.component.html',
  styleUrls: ['./specialities.component.css']
})
export class SpecialitiesComponent {


  specialistsPaginatedList: PaginatedList<Speciality> | null = null;
  specialistsFilter: SpecialistFilter = {
    pageNumber: 1,
    pageSize: 12,
    name: ''
  }
  isLoadingSpecialists = false;

  filterForm = this.fb.group({
    name: ''
  });

  constructor(private specialistsService: SpecialistsService,
    private fb: FormBuilder) { }

  ngOnInit() {
    this.loadSpecialists();
  }

  applyFilter() {

  }

  loadSpecialists() {
    this.isLoadingSpecialists = true;
    this.specialistsService.get(this.specialistsFilter)
      .pipe(take(1),
        finalize(() => this.isLoadingSpecialists = false))
      .subscribe(res => {
        if (this.specialistsPaginatedList)
          res.data.unshift(...this.specialistsPaginatedList.data);

        this.specialistsPaginatedList = res;
      });
  }

  loadMoreSpecialists() {
    if (this.specialistsPaginatedList?.hasNextPage && !this.isLoadingSpecialists) {
      this.specialistsFilter.pageNumber += 1;
      this.loadSpecialists();
    }
  }
}
