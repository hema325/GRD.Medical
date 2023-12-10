import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'BytesSize'
})
export class BytesSizePipe implements PipeTransform {

  transform(value: number, decimals: number): unknown {
    if (!+value) return '0 Bytes'

    const k = 1024
    const dm = decimals < 0 ? 0 : decimals
    const sizes = ['Bytes', 'KiB', 'MiB', 'GiB', 'TiB', 'PiB', 'EiB', 'ZiB', 'YiB']

    const i = Math.floor(Math.log(value) / Math.log(k))

    return `${parseFloat((value / Math.pow(k, i)).toFixed(dm))} ${sizes[i]}`
  }

}
