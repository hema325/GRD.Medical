import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorsComponent } from './doctors.component';
import { RouterModule } from '@angular/router';

const routes = [
  { path: '', component: DoctorsComponent }
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class DoctorsRoutingModule { }
