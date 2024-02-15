import { Component, Input, OnInit, ViewChild } from '@angular/core';
import ICardThumb from 'src/app/ordering/cart/types/ICardThumb';
import IOrderRequest from './types/checkoutForm';
import { FormsModule, NgForm } from '@angular/forms';
import { PaymentTypeComponent } from '../payment-type/payment-type.component';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { ProductCardThumbComponent } from 'src/app/shared/product/components/thumbs/product-card-thumb/product-card-thumb.component';
import { CartService } from 'src/app/shared/cart/services/cart.service';
import ICartLine from 'src/app/ordering/cart/types/ICartLine';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { OrderingService } from 'src/app/ordering/shared/services/ordering.service';
import { pairwise, startWith } from 'rxjs';
import { SummaryCardComponent } from 'src/app/ordering/shared/components/summary-card/summary-card.component';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    PaymentTypeComponent,
    ProductCardThumbComponent,
    UiKitModule,
    FormsModule,
    RouterModule,
    SummaryCardComponent
  ],
  selector: 'app-checkout-form',
  templateUrl: './checkout-form.component.html',
  styleUrls: ['./checkout-form.component.scss']
})
export class CheckoutFormComponent implements OnInit {
  cart: ICartLine[] = [];
  canCreateOrder: boolean = false;
  @ViewChild('checkoutForm', { static: true }) checkoutForm?: NgForm;

  model: IOrderRequest = {
    firstname: '',
    secondname: '',
    patronimyc: null,
    fullAddress: '',
    house: '',
    pavilion: null,
    flat: null,
    payment: "online"
  }

  constructor(
    private cartService: CartService,
    private orderingService: OrderingService,
    private router: Router
  ) {}

  get checkoutItems() {
    return this.cart?.map(
      x => {
        return {
          id: x.productId,
          imgSrc: x.imgSrc
        }
      }
    );
  }

  ngOnInit(): void {
    this.cartService.getCart().subscribe(
      (cart: ICartLine[]) => this.cart = cart
    );

    this.cartService.cart.subscribe(
      (cart: ICartLine[]) => this.cart = cart
    );

    this.checkoutForm?.form.valueChanges?.subscribe(
      () => {
        this.canCreateOrder = this.checkoutForm?.form.status !== 'INVALID';
      }
    )
  }

  onSubmit() {
    if (this.checkoutForm?.form.status !== 'VALID') {
      return;
    }
    console.log(this.checkoutForm);
    alert('1');
    //this.router.navigate(['/ordering/payment']);
  }
}