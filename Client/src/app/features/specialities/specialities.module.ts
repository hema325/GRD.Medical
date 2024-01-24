import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SpecialitiesRoutingModule } from './specialities-routing.module';
import { SpecialitiesComponent } from './specialities.component';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { SpecialistSelectInputComponent } from './specialist-select-input/specialist-select-input.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    SpecialitiesComponent,
    SpecialistSelectInputComponent,
  ],
  imports: [
    CommonModule,
    SpecialitiesRoutingModule,
    CoreModule,
    SharedModule,
    RouterModule
  ],
  exports: [
    SpecialistSelectInputComponent
  ]
})
export class SpecialitiesModule { }
