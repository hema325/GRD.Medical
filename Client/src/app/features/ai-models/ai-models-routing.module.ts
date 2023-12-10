import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AiModelsComponent } from './ai-models.component';
import { ChatingComponent } from './chating/chating.component';
import { HeartCheckingComponent } from './heart-checking/heart-checking.component';
import { SkinCheckingComponent } from './skin-checking/skin-checking.component';


const routes = [
  { path: '', component: AiModelsComponent },
  { path: 'chating', component: ChatingComponent },
  { path: 'heart-checking', component: HeartCheckingComponent },
  { path: 'skin-checking', component: SkinCheckingComponent },
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AiModelsRoutingModule { }
