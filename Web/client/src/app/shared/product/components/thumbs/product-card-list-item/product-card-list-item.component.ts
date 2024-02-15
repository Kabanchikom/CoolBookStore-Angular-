import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';

@Component({
  standalone: true,
  imports: [UiKitModule],
  selector: 'app-product-card-list-item',
  templateUrl: './product-card-list-item.component.html',
  styleUrls: ['./product-card-list-item.component.scss']
})
export class ProductCardListItemComponent {
  @Input() productCard?: {
    id: string,
    name: string,
    imgSrc: string,
    oldPrice: number,
    newPrice?: number,
    isOnSale: boolean,
    description?: string
  };

  @Input() enableBorder?: boolean;
  @Output() onDeleteClick = new EventEmitter<void>();
}