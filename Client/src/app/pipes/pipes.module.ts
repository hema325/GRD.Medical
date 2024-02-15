import { NgModule } from '@angular/core';
import { UtctimeToLocaltimePipe } from './utctime-to-localtime.pipe';
import { TimeFormaterPipe } from './time-formater.pipe';
import { BytesSizePipe } from './bytes-size.pipe';



@NgModule({
  declarations: [
    BytesSizePipe,
    TimeFormaterPipe,
    UtctimeToLocaltimePipe
  ],
  exports: [
    BytesSizePipe,
    TimeFormaterPipe,
    UtctimeToLocaltimePipe
  ]
})
export class PipesModule { }
