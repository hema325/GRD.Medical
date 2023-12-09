import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { MaterialModule } from 'src/app/shared/material.module';
import { ServicesComponent } from './services/services.component';
import { AboutComponent } from './about/about.component';



@NgModule({
  declarations: [
    HomeComponent,
    ServicesComponent,
    AboutComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    MaterialModule
  ]
})
export class HomeModule { }
