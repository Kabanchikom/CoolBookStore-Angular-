import { Component, Input, Output, EventEmitter } from '@angular/core';
import IProductCatalogItem from '../../types/IProductCatalogItem';
import { CartService } from 'src/app/shared/cart/services/cart.service';
import { AccountService } from 'src/app/shared/account/services/account.service';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';

@Component({
  standalone: true,
  imports: [ UiKitModule ],
  selector: 'app-product-card-box',
  templateUrl: './product-card-box.component.html',
  styleUrls: ['./product-card-box.component.scss']
})
export class ProductCardBoxComponent {
  @Input() product?: IProductCatalogItem;
  @Output() cardClick = new EventEmitter<string>();

  constructor(
    private cartService: CartService,
    private accountService: AccountService
  ) {}
  
  onAddClick() {
    this.product && this.cartService.add({
      productId: this.product.id,
      quantity: 1,
      ...this.product
    }).subscribe()
  }
}
