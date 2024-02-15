import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuardService } from "../shared/account/services/auth-guard.service";
import { AccountSettingsComponent } from "./account-settings/components/account-settings/account-settings.component";
import { FavoriteListComponent } from "./favorite/components/favorite-list/favorite-list.component";
import { OrderListComponent } from "./orders/components/order-list/order-list.component";
import { AccountPageComponent } from "./pages/account-page/account-page.component";

export const ACCOUNT_ROUTES: Routes = [
  {
    path: '',
    component: AccountPageComponent,
    canActivate: [AuthGuardService],
    canActivateChild: [AuthGuardService],
    children: [
      { path: 'orders', component: OrderListComponent },
      { path: 'favorites', component: FavoriteListComponent },
      { path: 'settings', component: AccountSettingsComponent },
    ]
  }
];