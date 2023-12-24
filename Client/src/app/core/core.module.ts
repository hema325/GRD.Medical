import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { MaterialModule } from '../shared/material.module';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';



@NgModule({
  declarations: [
    NavbarComponent,
    FooterComponent,
    HeaderComponent,
    NotFoundComponent,
    ServerErrorComponent,
    NavMenuComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule
  ],
  exports: [
    NavbarComponent,
    FooterComponent,
    HeaderComponent
  ]
})
export class CoreModule { }
