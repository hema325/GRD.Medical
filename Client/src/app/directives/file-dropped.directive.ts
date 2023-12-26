import { Directive, EventEmitter, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[fileDropped]'
})
export class FileDroppedDirective {

  @Output() fileDropped = new EventEmitter<FileList | undefined>();

  @HostListener('drop', ['$event']) onDrop(event: DragEvent) {
    event.preventDefault();
    this.fileDropped.emit(event.dataTransfer?.files);
  }

  @HostListener('dragover', ['$event']) onDragOver(event: DragEvent) {
    event.preventDefault();
  }


}
