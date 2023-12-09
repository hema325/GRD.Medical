import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ArticlesComponent } from './articles.component';
import { ArticleDetailsComponent } from './article-details/article-details.component';

const routes = [
  { path: '', component: ArticlesComponent },
  { path: ':id', component: ArticleDetailsComponent },
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ArticlesRoutingModule { }
