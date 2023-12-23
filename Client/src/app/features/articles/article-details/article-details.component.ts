import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Article } from 'src/app/models/articles/article';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css']
})
export class ArticleDetailsComponent {
  article: Article | null = null;

  constructor(private articlesService: ArticlesService,
    private activatedRoute: ActivatedRoute,
    private router: Router) {
    this.article = this.router.getCurrentNavigation()?.extras.state as Article;
  }

  ngOnInit() {
    let id = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    if (!this.article)
      this.articlesService.getById(id).subscribe(res => this.article = res);
  }
}
