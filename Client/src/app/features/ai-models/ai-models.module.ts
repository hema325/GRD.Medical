import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AiModelsComponent } from './ai-models.component';
import { AiModelsRoutingModule } from './ai-models-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CoreModule } from 'src/app/core/core.module';
import { ChatingComponent } from './chating/chating.component';
import { HeartCheckingComponent } from './heart-checking/heart-checking.component';
import { SkinCheckingComponent } from './skin-checking/skin-checking.component';


@NgModule({
  declarations: [
    AiModelsComponent,
    ChatingComponent,
    HeartCheckingComponent,
    SkinCheckingComponent
  ],
  imports: [
    CommonModule,
    AiModelsRoutingModule,
    SharedModule,
    CoreModule
  ]
})
export class AiModelsModule { }
