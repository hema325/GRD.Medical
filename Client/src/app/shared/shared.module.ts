import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormInputComponent } from './forms/form-input/form-input.component';
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
import { FormSelectComponent } from './forms/form-select/form-select.component';
import { TextAreaInputComponent } from './forms/text-area-input/text-area-input.component';
import { ExperienceSelectInputComponent } from './experience-select-input/experience-select-input.component';
import { TimePickerInputComponent } from './forms/time-picker-input/time-picker-input.component';
import { TimeFormaterPipe } from '../pipes/time-formater.pipe';
import { DatePickerInputComponent } from './forms/date-picker-input/date-picker-input.component';
import { NoContentCardComponent } from './no-content-card/no-content-card.component';
import { WritingLoaderComponent } from './chat-components/writing-loader/writing-loader.component';
import { ScrollDownBtnComponent } from './chat-components/scroll-down-btn/scroll-down-btn.component';
import { MessageCardComponent } from './chat-components/message-card/message-card.component';
import { MessageSenderComponent } from './chat-components/message-sender/message-sender.component';
import { NgxStarsModule } from 'ngx-stars';

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
    TimePickerInputComponent,
    TimeFormaterPipe,
    DatePickerInputComponent,
    NoContentCardComponent,
    WritingLoaderComponent,
    ScrollDownBtnComponent,
    MessageCardComponent,
    MessageSenderComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule,
    TimeagoModule.forChild(),
    CarouselModule,
    FormsModule,
    NgxStarsModule
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
    TimePickerInputComponent,
    TimeFormaterPipe,
    DatePickerInputComponent,
    ReactiveFormsModule,
    NoContentCardComponent,
    WritingLoaderComponent,
    ScrollDownBtnComponent,
    MessageCardComponent,
    MessageSenderComponent,
    NgxStarsModule
  ]
})
export class SharedModule { }
