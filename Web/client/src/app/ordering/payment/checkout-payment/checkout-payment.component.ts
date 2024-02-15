import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { Router } from '@angular/router';

@Component({
  selector: 'app-checkout-payment',
  standalone: true,
  imports: [CommonModule, UiKitModule],
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent {
  constructor(
    private router: Router
  ) {}

  onPayment() {
    this.router.navigate(['/ordering/completed']);
  }
}
