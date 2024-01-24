import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { finalize, map, take } from 'rxjs';
import { FilterBase } from 'src/app/models/filter-base';
import { PaginatedList } from 'src/app/models/paginated-list';
import { LanguagesService } from 'src/app/services/languages.service';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-language-select-input',
  templateUrl: './language-select-input.component.html',
  styleUrls: ['./language-select-input.component.css']
})
export class LanguageSelectInputComponent {

  @Input() control!: FormControl;
  @Input() loadAll: boolean = false;

  constructor(private languagesService: LanguagesService,
    private loader: LoaderService) { }

  ngOnInit() {
    if (this.loadAll)
      this.languagesFilter.pageSize = 2147483647;

    this.loadlanguages();
  }

  languagesPaginatedList: PaginatedList<any> | null = null;
  languagesFilter: FilterBase = {
    pageNumber: 1,
    pageSize: 6
  }
  isLoadinglanguages = false;

  loadlanguages() {
    this.isLoadinglanguages = true;
    this.loader.skipNextRequest();
    this.languagesService.get(this.languagesFilter)
      .pipe(take(1),
        finalize(() => this.isLoadinglanguages = false),
        map(res => {

          const mappedData = res.data.map(s => {
            return { value: s.id, text: s.name }
          });

          return { ...res, data: mappedData };
        }))
      .subscribe(res => {
        if (this.languagesPaginatedList)
          res.data.unshift(...this.languagesPaginatedList.data);

        this.languagesPaginatedList = res;
      });
  }

  loadMorelanguages() {
    if (this.languagesPaginatedList?.hasNextPage && !this.isLoadinglanguages) {
      this.languagesFilter.pageNumber += 1;
      this.loadlanguages();
    }
  }

}
