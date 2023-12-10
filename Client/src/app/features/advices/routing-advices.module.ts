import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AdvicesComponent } from './advices.component';
import { AdvicesDetailsComponent } from './advices-details/advices-details.component';

const routes = [
  { path: '', component: AdvicesComponent },
  { path: ':id', component: AdvicesDetailsComponent },
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
export class RoutingAdvicesModule { }
