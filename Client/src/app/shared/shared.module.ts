import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormInputComponent } from './form-input/form-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { EmojisListComponent } from './emojis-list/emojis-list.component';
import { BytesSizePipe } from '../pipes/bytes-size.pipe';
import { ImageCardComponent } from './image-card/image-card.component';
import { AudioCardComponent } from './audio-card/audio-card.component';


@NgModule({
  declarations: [
    FormInputComponent,
    EmojisListComponent,
    BytesSizePipe,
    ImageCardComponent,
    AudioCardComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  exports: [
    FormInputComponent,
    MaterialModule,
    EmojisListComponent,
    BytesSizePipe,
    ImageCardComponent,
    AudioCardComponent
  ]
})
export class SharedModule { }
