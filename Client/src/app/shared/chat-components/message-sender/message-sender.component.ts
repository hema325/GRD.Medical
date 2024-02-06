import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';

type Message = {
  content: string
  files: File[] | null
}

@Component({
  selector: 'app-message-sender',
  templateUrl: './message-sender.component.html',
  styleUrls: ['./message-sender.component.css']
})
export class MessageSenderComponent {

  @Input() disable = false;
  @Output() sendBtnClicked = new EventEmitter<Message>();
  @Output() writingStatus = new EventEmitter<boolean>();
  @Input() enableMedia = false;

  message: Message = {
    content: '',
    files: null
  }

  send() {
    this.message.content = this.message.content.trim();
    this.sendBtnClicked.emit(this.message);
    this.resetTextArea();
  }

  sendFiles(e: any) {
    if (e.target.files.length) {
      this.message.files = [...e.target.files];
      this.sendBtnClicked.emit(this.message);
      this.message.files = null;
    }
  }

  @ViewChild('textarea') textArea?: ElementRef;

  resetTextArea() {
    const ele = this.textArea?.nativeElement;
    ele.value = '';
    ele.dispatchEvent(new Event('input'));
  }

  handleWritingStatus(e: any) {
    if (e.target.value)
      this.writingStatus.emit(true);
    else
      this.writingStatus.emit(false);
  }

}
