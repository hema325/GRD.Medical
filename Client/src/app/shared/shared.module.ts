import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormInputComponent } from './form-input/form-input.component';
import { ReactiveFormsModule } from '@angular/forms';
<<<<<<< HEAD

=======
import { MaterialModule } from './material.module';
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf


@NgModule({
  declarations: [
    FormInputComponent
  ],
  imports: [
    CommonModule,
<<<<<<< HEAD
    ReactiveFormsModule
  ],
  exports: [
    FormInputComponent
=======
    ReactiveFormsModule,
    MaterialModule
  ],
  exports: [
    FormInputComponent,
    MaterialModule
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf
  ]
})
export class SharedModule { }
