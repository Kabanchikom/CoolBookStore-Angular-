import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CartIconComponent } from '../cart-icon/cart-icon.component';

@Component({
  standalone: true,
  imports: [CartIconComponent],
  selector: 'app-cart-widget',
  templateUrl: './cart-widget.component.html',
  styleUrls: ['./cart-widget.component.scss']
})
export class CartWidgetComponent {
  @Input() count = 0;
  @Output() click = new EventEmitter<void>();
  
  onClick(e: Event) {
    e.preventDefault();
    this.click.emit();
  }
}