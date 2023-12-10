import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoutingAdvicesModule } from './routing-advices.module';
import { AdvicesComponent } from './advices.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CoreModule } from 'src/app/core/core.module';
import { AdvicesDetailsComponent } from './advices-details/advices-details.component';

@NgModule({
  declarations: [
    AdvicesComponent,
    AdvicesDetailsComponent
  ],
  imports: [
    CommonModule,
    RoutingAdvicesModule,
    SharedModule,
    CoreModule
  ]
})
export class AdvicesModule { }
