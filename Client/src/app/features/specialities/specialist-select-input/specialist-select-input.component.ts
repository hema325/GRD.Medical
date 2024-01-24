import { Component, Input } from '@angular/core';
import { AbstractControl, FormControl } from '@angular/forms';
import { finalize, map, take } from 'rxjs';
import { PaginatedList } from 'src/app/models/paginated-list';
import { Speciality } from 'src/app/models/specialists/Speciality';
import { SpecialistFilter } from 'src/app/models/specialists/specialist-filter';
import { LoaderService } from 'src/app/services/loader.service';
import { SpecialistsService } from 'src/app/services/specialists.service';

@Component({
  selector: 'app-specialist-select-input',
  templateUrl: './specialist-select-input.component.html',
  styleUrls: ['./specialist-select-input.component.css']
})
export class SpecialistSelectInputComponent {

  @Input() control!: FormControl;
  @Input() loadAll = false;
  @Input() default?: string;

  constructor(private specialistsService: SpecialistsService,
    private loader: LoaderService) { }

  specialistsPaginatedList: PaginatedList<any> | null = null;
  specialistsFilter: SpecialistFilter = {
    pageNumber: 1,
    pageSize: 6,
    name: ''
  }
  isLoadingSpecialists = false;

  ngOnInit() {
    if (this.loadAll)
      this.specialistsFilter.pageSize = 2147483647;

    this.loadSpecialists();
  }

  loadSpecialists() {
    this.isLoadingSpecialists = true;
    this.loader.skipNextRequest();
    this.specialistsService.get(this.specialistsFilter)
      .pipe(take(1),
        finalize(() => this.isLoadingSpecialists = false),
        map(res => {

          const mappedData = res.data.map(s => {
            return { value: s.id, text: s.name }
          });

          return { ...res, data: mappedData };
        }))
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
