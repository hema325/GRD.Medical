import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { MaterialModule } from '../shared/material.module';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FloatingButtonNavigationComponent } from './floating-button-navigation/floating-button-navigation.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    NavbarComponent,
    FooterComponent,
    NotFoundComponent,
    ServerErrorComponent,
    NavMenuComponent,
    FloatingButtonNavigationComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    SharedModule
  ],
  exports: [
    NavbarComponent,
    FooterComponent,
    FloatingButtonNavigationComponent
  ]
})
export class CoreModule { }
