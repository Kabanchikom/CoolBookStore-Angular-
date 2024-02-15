import { Component, EventEmitter, Input, Output } from '@angular/core';
import IProductCatalogItem from 'src/app/product/types/IProductCatalogItem';
import ICartLine from '../../types/ICartLine';
import { CartService } from 'src/app/shared/cart/services/cart.service';
import { CommonModule } from '@angular/common';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { ProductCardListItemComponent } from 'src/app/shared/product/components/thumbs/product-card-list-item/product-card-list-item.component';

@Component({
  standalone: true,
  imports: [ CommonModule, UiKitModule, ProductCardListItemComponent ],
  selector: 'app-cart-item-card',
  templateUrl: './cart-item-card.component.html',
  styleUrls: ['./cart-item-card.component.scss']
})
export class CartItemCardComponent {
  @Input() cartLine?: ICartLine;

  productCard?: IProductCatalogItem;

  constructor(
    private cartService: CartService
  ) { }

  onPlusButtonClick() {
    if (!this.cartLine) {
      return;
    }
    
    let newQuantity = ++this.cartLine.quantity;
    this.cartService.update(this.cartLine?.id, {
        productId: this.cartLine.productId,
        quantity: newQuantity
      }).subscribe();
  }

  onMinusButtonClick() {
    if (!this.cartLine || this.cartLine.quantity <= 1) {
      return;
    }

    let newQuantity = --this.cartLine.quantity;
    
    this.cartService.update(this.cartLine?.id, {
        productId: this.cartLine.productId,
        quantity: newQuantity
      }).subscribe();
  }

  onDeleteClick() {
    this.cartLine && 
    this.cartService.deleteLine(this.cartLine?.productId).subscribe();
  }

  ngOnInit() {
    this.productCard = {...this.cartLine!, isOnSale: false}
  }
}