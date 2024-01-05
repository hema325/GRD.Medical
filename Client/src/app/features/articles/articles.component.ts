import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Article } from 'src/app/models/articles/article';
import { ArticleFilter } from 'src/app/models/articles/article-filter';
import { PaginatedList } from 'src/app/models/paginated-list';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})
export class ArticlesComponent {
  date = new Date();

  filterForm = this.fb.group({
    title: ['']
  });

  filter: ArticleFilter = {
    title: '',
    pageNumber: 1,
    pageSize: 6
  }

  paginatedList: PaginatedList<Article> | null = null;

  constructor(private fb: FormBuilder,
    private articlesService: ArticlesService) { }

  ngOnInit() {
    this.getArticles();
  }

  getArticles() {
    this.articlesService.getArticles(this.filter).subscribe(res => this.paginatedList = res);
  }

  handlePageEvent(event: PageEvent) {
    this.filter.pageSize = event.pageSize;
    this.filter.pageNumber = event.pageIndex + 1;
    this.getArticles();
  }

  handleSearchFilter() {
    this.filter.title = this.filterForm.controls.title.value;
    this.filter.pageNumber = 1;
    this.getArticles();
  }
}
