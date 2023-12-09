import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
<<<<<<< HEAD

const routes: Routes = [
  { path: '', redirectTo: 'account', pathMatch: "full" },
  { path: 'account', loadChildren: () => import('./features/account/account.module').then(m => m.AccountModule) }
=======
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { tryToLoginGuard } from './guards/try-to-login.guard';
import { authGuard } from './guards/auth.guard';

const routes: Routes = [
  {
    path: '', children: [
      { path: '', redirectTo: 'home', pathMatch: "full" },
      { path: 'account', loadChildren: () => import('./features/account/account.module').then(m => m.AccountModule) },
      { path: 'home', loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule) },
      { path: 'articles', loadChildren: () => import('./features/articles/articles.module').then(m => m.ArticlesModule) },
      { path: 'advices', loadChildren: () => import('./features/advices/advices.module').then(m => m.AdvicesModule) },
      { path: 'ai-models', loadChildren: () => import('./features/ai-models/ai-models.module').then(m => m.AiModelsModule), canActivate: [authGuard] },
      { path: 'not-found', component: NotFoundComponent },
      { path: 'server-error', component: ServerErrorComponent },
      { path: '**', component: NotFoundComponent },
    ], canMatch: [tryToLoginGuard]
  }

>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
