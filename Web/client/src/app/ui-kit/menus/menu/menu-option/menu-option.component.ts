import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-menu-option',
  templateUrl: './menu-option.component.html',
  styleUrls: ['./menu-option.component.scss']
})
export class MenuOptionComponent {
  @Output() click = new EventEmitter<Event>()
}