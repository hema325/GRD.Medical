import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { authGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: "full" },
  { path: 'account', loadChildren: () => import('./features/account/account.module').then(m => m.AccountModule) },
  { path: 'ai-models', loadChildren: () => import('./features/ai-models/ai-models.module').then(m => m.AiModelsModule), canActivate: [authGuard] },
  { path: 'home', loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule) },
  { path: 'articles', loadChildren: () => import('./features/articles/articles.module').then(m => m.ArticlesModule) },
  { path: 'advices', loadChildren: () => import('./features/advices/advices.module').then(m => m.AdvicesModule) },
  { path: 'posts', loadChildren: () => import('./features/posts/posts.module').then(m => m.PostsModule), canActivate: [authGuard] },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
