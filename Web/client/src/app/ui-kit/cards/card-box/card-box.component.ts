import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-card-box',
  templateUrl: './card-box.component.html',
  styleUrls: ['./card-box.component.scss']
})
export class CardBoxComponent {
  @Input() imgSrc: string = '';
  @Output() onCardClick = new EventEmitter<void>();

  handleClick(e: Event) {
    e.preventDefault();

    if (this.onCardClick) {
        this.onCardClick.emit();
    }
  }
}