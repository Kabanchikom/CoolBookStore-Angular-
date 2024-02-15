import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-row-card',
  templateUrl: './row-card.component.html',
  styleUrls: ['./row-card.component.scss']
})
export class RowCardComponent {
  @Input() imgSrc?: string;
  @Output() onDeleteClick = new EventEmitter();
}