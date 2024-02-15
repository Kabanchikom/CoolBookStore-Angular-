import { Routes } from "@angular/router";
import { AuthGuardService } from "../shared/account/services/auth-guard.service";
import { ToDoPageComponent } from "../ui-kit/pages/to-do-page/to-do-page.component";
import { CartListComponent } from "./cart/components/cart-list/cart-list.component";
import { CheckoutFormComponent } from "./checkout/components/checkout-form/checkout-form.component";
import { OrderingPageComponent } from "./pages/ordering-page/ordering-page.component";
import { CheckoutPaymentComponent } from "./payment/checkout-payment/checkout-payment.component";
import { OrderingCompletedComponent } from "./completed/ordering-completed/ordering-completed.component";

export const ORDERING_ROUTES: Routes = [
    {
        path: '',
        component: OrderingPageComponent,
        canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        children: [
            { path: 'cart', component: CartListComponent },
            { path: 'checkout', component: CheckoutFormComponent },
            { path: 'payment', component: CheckoutPaymentComponent },
            { path: 'completed', component: OrderingCompletedComponent },
        ]
    },
];