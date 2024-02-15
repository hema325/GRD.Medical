import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AutoResizeTextAreaDirective } from './auto-resize-text-area.directive';
import { FileDroppedDirective } from './file-dropped.directive';
import { LongPressDirective } from './long-press.directive';
import { ReachedBottomDirective } from './reached-bottom.directive';



@NgModule({
  declarations: [
    AutoResizeTextAreaDirective,
    FileDroppedDirective,
    LongPressDirective,
    ReachedBottomDirective
  ],
  exports: [
    AutoResizeTextAreaDirective,
    FileDroppedDirective,
    LongPressDirective,
    ReachedBottomDirective
  ]
})
export class DirectivesModule { }
