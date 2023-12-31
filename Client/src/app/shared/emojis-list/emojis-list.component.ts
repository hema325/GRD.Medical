import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-emojis-list',
  templateUrl: './emojis-list.component.html',
  styleUrls: ['./emojis-list.component.css']
})
export class EmojisListComponent {

  emojis: string[] = [
    '😀', '😃', '😄', '😁', '😆', '😅', '😂', '🤣', '😊', '😇',
    '🙂', '🙃', '😉', '😌', '😍', '🥰', '😘', '😗', '😙', '😚',
    '😋', '😛', '😝', '😜', '🤪', '🤨', '🧐', '🤓', '😎', '🤩',
    '🥳', '😏', '😒', '😞', '😔', '😟', '😕', '🙁', '☹️', '😣',
    '😖', '😫', '😩', '🥺', '😢', '😭', '😤', '😠', '😡', '🤬',
    '🤯', '😳', '🥵', '🥶', '😱', '😨', '😰', '😥', '😓', '🤗',
    '🤔', '🤭', '🤫', '🤥', '😶', '😐', '😑', '😬', '🙄', '😯',
    '😦', '😧', '😮', '😲', '🥱', '😴', '🤤', '😪', '😵', '🤐',
    '🥴', '🤢', '🤮', '🤧', '😷', '🤒', '🤕', '🤑', '🤠', '😈',
    '👿', '👹', '👺', '🤡', '💩', '👻', '💀', '☠️', '👽', '👾',
    '🤖', '🎃', '😺', '😸', '😹', '😻', '😼', '😽', '🙀', '😿',
    '😾', '👐', '🙌', '👏', '🤝', '👍', '👎', '👊', '✊', '🤛',
    '🤜', '🤞', '✌️', '🤟', '🤘', '👌', '👈', '👉', '👆', '👇',
    '☝️', '✋', '🤚', '🖐️', '🖖', '👋', '🤙', '💪', '🦾', '🖕',
    '✍️', '🤳', '💅', '💍', '💄', '💋', '👄', '🦷', '👅', '👂',
    '🦻', '👃', '👁️', '👀', '🧠', '🫀', '🫁', '🦴', '🦷', '🗣️',
    '👤', '👥', '🫂', '👶', '👧', '🧒', '👦', '👩', '🧑', '👨',
    '👩‍🦱', '🧑‍🦱', '👨‍🦱', '👩‍🦰', '🧑‍🦰', '👨‍🦰', '👱‍♀️', '👱',
    '👱‍♂️', '👩‍🦳', '🧑‍🦳', '👨‍🦳', '👩‍🦲', '🧑‍🦲', '👨‍🦲', '🧔',
    '👵', '🧓', '👴', '👲', '👳‍♀️', '👳', '👳‍♂️', '🧕', '🤵‍♀️', '🤵',
    '🤵‍♂️', '👰‍♀️', '👰', '👰‍♂️', '🤰', '🤱', '👩‍🍼', '🧑‍🍼', '👨‍🍼',
    '🧑‍🎓', '👩‍🎓', '👨‍🎓', '👩‍🏫', '🧑‍🏫', '👨‍🏫', '👩‍⚕️', '🧑‍⚕️', '👨‍⚕️',
    '👩‍🌾', '🧑‍🌾', '👨‍🌾', '👩‍🍳', '🧑‍🍳', '👨‍🍳', '👩‍🎤', '🧑‍🎤', '👨‍🎤',
    '👩‍🎨', '🧑‍🎨', '👨‍🎨', '👩‍✈️', '🧑‍✈️', '👨‍✈️', '👩‍🚀', '🧑‍🚀', '👨‍🚀',
    '👩‍🚒', '🧑‍🚒', '👨‍🚒', '👮‍♀️', '👮', '👮‍♂️', '🕵️‍♀️', '🕵️', '🕵️‍♂️',
    '💂‍♀️', '💂', '💂‍♂️', '👷‍♀️', '👷', '👷‍♂️', '🤴', '👸', '🦸‍♀️', '🦸',
    '🦸‍♂️', '🦹‍♀️', '🦹', '🦹‍♂️', '🤶', '🎅', '🧙‍♀️', '🧙', '🧙‍♂️', '🧚‍♀️',
    '🧚', '🧚‍♂️', '🧛‍♀️', '🧛', '🧛‍♂️', '🧜‍♀️', '🧜', '🧜‍♂️', '🧝‍♀️', '🧝',
    '🧝‍♂️', '💆‍♀️', '💆', '💆‍♂️', '💇‍♀️', '💇', '💇‍♂️', '🚶‍♀️', '🚶', '🚶‍♂️',
    '🧍‍♀️', '🧍', '🧍‍♂️', '🧎‍♀️', '🧎', '🧎‍♂️', '👩‍🦯', '🧑‍🦯', '👨‍🦯', '👩‍🦼',
    '🧑‍🦼', '👨‍🦼', '👩‍🦽', '🧑‍🦽', '👨‍🦽', '🏃‍♀️', '🏃', '🏃‍♂️', '💃', '🕺',
    '🕴️', '👯‍♀️', '👯', '👯‍♂️', '🧖‍♀️', '🧖', '🧖‍♂️', '🧘‍♀️', '🧘', '🧘‍♂️',
    '🛀', '🛌', '🧑‍🤝‍🧑', '👭', '👫', '👬', '💏', '👩‍❤️‍💋‍👨', '👨‍❤️‍💋‍👨', '👩‍❤️‍💋‍👩',
    '💑', '👩‍❤️‍👨', '👨‍❤️‍👨', '👩‍❤️‍👩', '👪', '👨‍👩‍👦', '👨‍👩‍👧', '👨‍👩‍👧‍👦', '👨‍👩‍👦‍👦', '👨‍👩‍👧‍👧',
    '👩‍👩‍👦', '👩‍👩‍👧', '👩‍👩‍👧‍👦', '👩‍👩‍👦‍👦', '👩‍👩‍👧‍👧', '👨‍👨‍👦', '👨‍👨‍👧', '👨‍👨‍👧‍👦', '👨‍👨‍👦‍👦', '👨‍👨‍👧‍👧',
    '👩‍👦', '👩‍👧', '👩‍👧‍👦', '👩‍👦‍👦', '👩‍👧‍👧'
  ];

  @Input() active: boolean = false;
  @Output() emojiSelected = new EventEmitter<string>();

  select(emoji: string) {
    this.emojiSelected.emit(emoji);
  }
}
