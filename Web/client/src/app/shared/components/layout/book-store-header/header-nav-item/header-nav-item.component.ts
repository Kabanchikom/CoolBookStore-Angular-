import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-header-nav-item',
  templateUrl: './header-nav-item.component.html',
  styleUrls: ['./header-nav-item.component.scss']
})
export class HeaderNavItemComponent {
  @Input() text = '';
}