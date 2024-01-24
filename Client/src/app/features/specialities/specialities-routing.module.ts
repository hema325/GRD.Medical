import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SpecialitiesComponent } from './specialities.component';

const routes = [
  { path: '', component: SpecialitiesComponent }
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ]
})
export class SpecialitiesRoutingModule { }
