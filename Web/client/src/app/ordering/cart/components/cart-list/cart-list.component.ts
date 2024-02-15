import { Component, OnDestroy, OnInit } from '@angular/core';
import ICartLine from '../../types/ICartLine';
import { CartService } from 'src/app/shared/cart/services/cart.service';
import { CartItemCardComponent } from '../cart-item-card/cart-item-card.component';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { CommonModule } from '@angular/common';
import { SummaryCardComponent } from 'src/app/ordering/shared/components/summary-card/summary-card.component';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  imports: [ CommonModule, CartItemCardComponent, UiKitModule, SummaryCardComponent ],
  selector: 'app-cart-list',
  templateUrl: './cart-list.component.html',
  styleUrls: ['./cart-list.component.scss']
})
export class CartListComponent implements OnInit {
  cartLines: ICartLine[] = [];

  constructor(
    private cartService: CartService,
    private router: Router
  ) {}
  
  ngOnInit(): void {
    this.cartService.getCart().subscribe(x => {
      this.cartLines = x;
    });

    this.cartService.cart.subscribe(
      x => {
        this.cartLines = x;
      }
    );
  }

  handleOrderUpdateItem() {

  }

  handleOrderDeleteItem(id: string) {

  }

  navigateToCheckout() {
    this.router.navigate(['/ordering/checkout']);
  }
}