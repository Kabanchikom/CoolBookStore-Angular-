import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ProductCardThumbComponent } from 'src/app/shared/product/components/thumbs/product-card-thumb/product-card-thumb.component';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';

@Component({
  standalone: true,
  imports: [CommonModule, ProductCardThumbComponent, UiKitModule],
  selector: 'app-order-card',
  templateUrl: './order-card.component.html',
  styleUrls: ['./order-card.component.scss']
})
export class OrderCardComponent {
  
}