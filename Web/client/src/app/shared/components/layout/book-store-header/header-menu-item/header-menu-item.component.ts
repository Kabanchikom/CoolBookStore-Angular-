import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-header-menu-item',
  templateUrl: './header-menu-item.component.html',
  styleUrls: ['./header-menu-item.component.scss']
})
export class HeaderMenuItemComponent {
  @Input() logoClass: string = "";
  @Input() text: string = "";

  click = new EventEmitter<void>();
}
