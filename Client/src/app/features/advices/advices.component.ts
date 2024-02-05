import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { finalize } from 'rxjs';
import { Advice } from 'src/app/models/advices/advice';
import { AdviceFilter } from 'src/app/models/advices/advice-filter';
import { PaginatedList } from 'src/app/models/paginated-list';
import { AdvicesService } from 'src/app/services/advices.service';

@Component({
  selector: 'app-advices',
  templateUrl: './advices.component.html',
  styleUrls: ['./advices.component.css']
})
export class AdvicesComponent {
  date = new Date();

  filterForm = this.fb.group({
    title: ['']
  })

  filter: AdviceFilter = {
    title: null,
    pageNumber: 1,
    pageSize: 6
  }

  paginatedList: PaginatedList<Advice> | null = null;
  isLoading = false;
  constructor(private fb: FormBuilder,
    private adviceService: AdvicesService) { }

  ngOnInit() {
    this.getAdvices();
  }

  getAdvices() {
    this.isLoading = true;
    this.adviceService.getAdvices(this.filter)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe(res => this.paginatedList = res);
  }

  handlePageEvent(event: PageEvent) {
    this.filter.pageSize = event.pageSize;
    this.filter.pageNumber = event.pageIndex + 1;
    this.getAdvices();
  }

  handleSearchFilter() {
    this.filter.title = this.filterForm.controls.title.value;
    this.filter.pageNumber = 1;
    this.getAdvices();
  }
}
