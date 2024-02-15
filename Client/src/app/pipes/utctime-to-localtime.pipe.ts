import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'utctimeToLocaltime'
})
export class UtctimeToLocaltimePipe implements PipeTransform {

  transform(time: string): Date {
    return new Date('1-30-2024 ' + time + ' UTC')
  }

}
