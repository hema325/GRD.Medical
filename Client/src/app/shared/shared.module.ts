import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormInputComponent } from './form-input/form-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { EmojisListComponent } from './emojis-list/emojis-list.component';
import { ImageCardComponent } from './image-card/image-card.component';
import { AudioCardComponent } from './audio-card/audio-card.component';
import { LongPressDirective } from '../directives/long-press.directive';
import { BytesSizePipe } from '../pipes/bytes-size.pipe';
import { AutoResizeTextAreaDirective } from '../directives/auto-resize-text-area.directive';
import { TimeagoModule } from "ngx-timeago";
import { FileDroppedDirective } from '../directives/file-dropped.directive';

@NgModule({
  declarations: [
    FormInputComponent,
    EmojisListComponent,
    ImageCardComponent,
    AudioCardComponent,
    LongPressDirective,
    BytesSizePipe,
    AutoResizeTextAreaDirective,
    FileDroppedDirective,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule,
    TimeagoModule.forChild()
  ],
  exports: [
    FormInputComponent,
    MaterialModule,
    EmojisListComponent,
    ImageCardComponent,
    AudioCardComponent,
    LongPressDirective,
    BytesSizePipe,
    AutoResizeTextAreaDirective,
    TimeagoModule,
    FileDroppedDirective
  ]
})
export class SharedModule { }
