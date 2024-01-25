import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormInputComponent } from './form-input/form-input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { EmojisListComponent } from './emojis-list/emojis-list.component';
import { ImageCardComponent } from './image-card/image-card.component';
import { AudioCardComponent } from './audio-card/audio-card.component';
import { LongPressDirective } from '../directives/long-press.directive';
import { BytesSizePipe } from '../pipes/bytes-size.pipe';
import { AutoResizeTextAreaDirective } from '../directives/auto-resize-text-area.directive';
import { TimeagoModule } from "ngx-timeago";
import { FileDroppedDirective } from '../directives/file-dropped.directive';
import { LightBoxComponent } from './light-box/light-box.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { ReachedBottomDirective } from '../directives/reached-bottom.directive';
import { FormSelectComponent } from './form-select/form-select.component';
import { TextAreaInputComponent } from './text-area-input/text-area-input.component';
import { ExperienceSelectInputComponent } from './experience-select-input/experience-select-input.component';
import { TimePickerInputComponent } from './time-picker-input/time-picker-input.component';

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
    LightBoxComponent,
    ReachedBottomDirective,
    FormSelectComponent,
    TextAreaInputComponent,
    ExperienceSelectInputComponent,
    TimePickerInputComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule,
    TimeagoModule.forChild(),
    CarouselModule
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
    FileDroppedDirective,
    LightBoxComponent,
    ReachedBottomDirective,
    FormSelectComponent,
    TextAreaInputComponent,
    ExperienceSelectInputComponent,
    TimePickerInputComponent
  ]
})
export class SharedModule { }
