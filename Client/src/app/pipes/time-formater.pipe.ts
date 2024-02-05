import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeFormater'
})
export class TimeFormaterPipe implements PipeTransform {

  transform(value: string): unknown {
    const timeSections = value.split(':');

    let hour = Number(timeSections[0]);
    let minute = Number(timeSections[1]);
    let amPmPart = hour >= 12 ? 'PM' : 'AM';

    if (hour > 12)
      hour = hour % 12;


    return `${hour <= 9 ? '0' : ''}${hour}:${minute <= 9 ? '0' : ''}${minute} ${amPmPart}`;
  }

}
