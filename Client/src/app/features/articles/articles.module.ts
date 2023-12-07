import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArticlesRoutingModule } from './articles-routing.module';
import { ArticlesComponent } from './articles.component';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ArticleDetailsComponent } from './article-details/article-details.component';



@NgModule({
  declarations: [
    ArticlesComponent,
    ArticleDetailsComponent
  ],
  imports: [
    CommonModule,
    ArticlesRoutingModule,
    CoreModule,
    SharedModule
  ]
})
export class ArticlesModule { }
