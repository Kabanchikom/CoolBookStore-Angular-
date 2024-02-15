import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { AuthInterceptorService } from "./shared/http/auth-interceptor.service";
import { AboutPageComponent } from "./shared/pages/about-page/about-page.component";
import { ContactsPageComponent } from "./shared/pages/contacts-page/contacts-page.component";
import { NotFoundPageComponent } from "./shared/pages/not-found-page/not-found-page.component";


const appRoutes: Routes = [
  { path: 'account', loadChildren: () => import('./account/account-routing').then(m => m.ACCOUNT_ROUTES) },
  { path: 'ordering', loadChildren: () => import('./ordering/ordering-routing').then(m => m.ORDERING_ROUTES) },
  { path: 'catalog', loadChildren: () => import('./product/product-routing').then(m => m.PRODUCT_ROUTES) },
  { path: 'about', component: AboutPageComponent },
  { path: 'contacts', component: ContactsPageComponent },
  { path: '', redirectTo: '/catalog', pathMatch: 'full' },
  { path: '**', component: NotFoundPageComponent },
];


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(
      appRoutes
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
