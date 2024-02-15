import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal-window',
  templateUrl: './modal-window.component.html',
  styleUrls: ['./modal-window.component.scss']
})
export class ModalWindowComponent {
    @Input() isActive: boolean = false;
    @Input() maxWidth?: string;
    @Output() closeClick = new EventEmitter<void>();

    onCloseClick() {
      this.closeClick.emit();
    }
}