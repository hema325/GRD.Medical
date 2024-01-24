import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorsComponent } from './doctors.component';
import { DoctorsRoutingModule } from './doctors-routing.module';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { DoctorsFilterComponent } from './doctors-filter/doctors-filter.component';
import { SpecialitiesModule } from '../specialities/specialities.module';

@NgModule({
  declarations: [
    DoctorsComponent,
    DoctorsFilterComponent
  ],
  imports: [
    CommonModule,
    DoctorsRoutingModule,
    CoreModule,
    SharedModule,
    SpecialitiesModule
  ]
})
export class DoctorsModule { }
