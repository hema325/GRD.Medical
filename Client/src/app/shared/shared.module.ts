import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormInputComponent } from './forms/form-input/form-input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { EmojisListComponent } from './emojis-list/emojis-list.component';
import { ImageCardComponent } from './image-card/image-card.component';
import { AudioCardComponent } from './audio-card/audio-card.component';
import { TimeagoModule } from "ngx-timeago";
import { LightBoxComponent } from './light-box/light-box.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { FormSelectComponent } from './forms/form-select/form-select.component';
import { TextAreaInputComponent } from './forms/text-area-input/text-area-input.component';
import { ExperienceSelectInputComponent } from './experience-select-input/experience-select-input.component';
import { TimePickerInputComponent } from './forms/time-picker-input/time-picker-input.component';
import { DatePickerInputComponent } from './forms/date-picker-input/date-picker-input.component';
import { NoContentCardComponent } from './no-content-card/no-content-card.component';
import { WritingLoaderComponent } from './chat-components/writing-loader/writing-loader.component';
import { ScrollDownBtnComponent } from './chat-components/scroll-down-btn/scroll-down-btn.component';
import { MessageCardComponent } from './chat-components/message-card/message-card.component';
import { MessageSenderComponent } from './chat-components/message-sender/message-sender.component';
import { NgxStarsModule } from 'ngx-stars';
import { PipesModule } from '../pipes/pipes.module';
import { DirectivesModule } from '../directives/directives.module';

@NgModule({
  declarations: [
    FormInputComponent,
    EmojisListComponent,
    ImageCardComponent,
    AudioCardComponent,
    LightBoxComponent,
    FormSelectComponent,
    TextAreaInputComponent,
    ExperienceSelectInputComponent,
    TimePickerInputComponent,
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
    NgxStarsModule,
    PipesModule,
    DirectivesModule
  ],
  exports: [
    PipesModule,
    DirectivesModule,
    FormInputComponent,
    MaterialModule,
    EmojisListComponent,
    ImageCardComponent,
    AudioCardComponent,
    TimeagoModule,
    LightBoxComponent,
    FormSelectComponent,
    TextAreaInputComponent,
    ExperienceSelectInputComponent,
    TimePickerInputComponent,
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
