import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { OrderCardComponent } from '../order-card/order-card.component';

@Component({
  standalone: true,
  imports: [CommonModule, UiKitModule, OrderCardComponent],
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent {
}
