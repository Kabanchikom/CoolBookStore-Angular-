import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import ICartLine from '../../cart/types/ICartLine';
import { CartService } from 'src/app/shared/cart/services/cart.service';
import { CommonModule } from '@angular/common';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { SummaryCardComponent } from '../../shared/components/summary-card/summary-card.component';
import { OrderingService } from '../../shared/services/ordering.service';

type Stage = "/ordering/cart" | "/ordering/checkout" | "/ordering/payment" | "/ordering/completed";

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule, UiKitModule, SummaryCardComponent],
  selector: 'app-ordering-page',
  templateUrl: './ordering-page.component.html',
  styleUrls: ['./ordering-page.component.scss']
})
export class OrderingPageComponent {
  cart: ICartLine[] = [];
  stage: Stage = "/ordering/cart";
  isNextDisabled: boolean = false;

  constructor(
    private cartService: CartService,
    private router: Router,
    private orderingService: OrderingService
  ) { }

  ngOnInit() {
    this.cartService.getCart().subscribe(
      (cart: ICartLine[]) => {
        this.cart = cart;
      }
    );

    this.cartService.cart.subscribe(
      (cart: ICartLine[]) => {
        this.cart = cart;
      }
    );

    this.stage = this.router.url as Stage;

    this.router.events.subscribe(
      x => {
        this.stage = this.router.url as Stage;
      }
    );

    this.orderingService.canCreateOrder.subscribe(
      x => {
        this.isNextDisabled = !x;
      }
    )
  }

  get showNextButton(): boolean {
    switch (this.stage) {
      case "/ordering/cart": case "/ordering/checkout": {
        return true;
      }
      default: {
        return false;
      }
    }
  }

  get nextStage(): Stage | null {
    switch (this.stage) {
      case "/ordering/cart": {
        return "/ordering/checkout";
      }
      case "/ordering/checkout": {
        return "/ordering/payment"
      }
      case "/ordering/payment": {
        return "/ordering/completed"
      }
      case "/ordering/completed": {
        return null;
      }
      default: {
        return null;
      }
    }
  }


  get nextButtonText(): string | null {
    switch (this.stage) {
      case "/ordering/cart": {
        return "Перейти к оформлению";
      }
      case "/ordering/checkout": {
        return "Перейти к оплате"
      }
      default: {
        return null;
      }
    }
  }

  setStage(stage: Stage | null) {
    if (!stage) {
      return;
    }

    this.stage = stage;
    this.router.navigate([stage]);
  }
}