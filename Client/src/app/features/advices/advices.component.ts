import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Advice } from 'src/app/models/advice';
import { Filter } from 'src/app/models/filter';
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
    title: ['', Validators.required]
  })

  filter: Filter = {
    title: null,
    pageNumber: 1,
    pageSize: 6
  }

  paginatedList: PaginatedList<Advice> | null = null;

  constructor(private fb: FormBuilder,
    private adviceService: AdvicesService) { }

  ngOnInit() {
    this.getAdvices();
  }

  getAdvices() {
    this.adviceService.getAdvices(this.filter).subscribe(res => this.paginatedList = res);
  }

  handlePageEvent(event: PageEvent) {
    this.filter.pageSize = event.pageSize;
    this.filter.pageNumber = event.pageIndex + 1;
    this.getAdvices();
  }

  handleResetFilter() {
    this.filter.title = null;
    this.filter.pageNumber = 1;
    this.filterForm.reset();
    this.getAdvices();
  }

  handleSearchFilter() {
    this.filter.title = this.filterForm.controls.title.value;
    this.filter.pageNumber = 1;
    this.getAdvices();
  }
}
