import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-popup',
  templateUrl: './popup.component.html',
  styleUrls: ['./popup.component.scss']
})
export class PopupComponent {
  @Input() isActive: boolean = false;
  @Output() closeClick = new EventEmitter<void>();
  @Output() click = new EventEmitter<void>();

  onClose(e: Event) {
    e.stopPropagation();
    this.closeClick.emit();
  }

  onClick(e: Event) {
    e.stopPropagation();
    this.click.emit();
  }
}