import { Component } from '@angular/core';
import IPaging from 'src/app/shared/types/IPaging';
import { IProductCatalogItemsFilter } from '../../services/http/types/productCatalog';
import IProductCatalogItem from '../../types/IProductCatalogItem';
import { ProductService } from '../../services/product.service';
import { CartService } from 'src/app/shared/cart/services/cart.service';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/shared/account/services/account.service';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { ProductListComponent } from '../../components/product-list/product-list.component';
import { CommonModule } from '@angular/common';
import { CartWidgetComponent } from 'src/app/shared/cart/components/cart-widget/cart-widget.component';
import { HttpClientModule } from '@angular/common/http';

@Component({
  standalone: true,
  imports: [ CommonModule, CartWidgetComponent, UiKitModule, ProductListComponent ],
  selector: 'app-catalog-page',
  templateUrl: './catalog-page.component.html',
  styleUrls: ['./catalog-page.component.scss']
})
export class CatalogPageComponent {
  products?: IProductCatalogItem[];
  total: number = 0;
  filter?: IProductCatalogItemsFilter;
  paging?: IPaging;
  cartCount = 0;
  showCartWidget = false;
  
  constructor(
    private productService: ProductService,
    private accountService: AccountService,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productService.getCatalogItems({
      paging: this.paging,
      filter: this.filter,
    }).subscribe(x => {
      this.products = x.products;
      this.total = x.total;
    });

    this.cartService.getCart().subscribe();
    
      this.cartService.getCount().subscribe(
        (count) => {
          this.cartCount = count;
        }
      );

    this.cartService.countSubject.subscribe(
      count => {
        this.cartCount = count;
      }
    );

    this.accountService._accessToken.subscribe(
      (token) => {
        if (token) {
          this.showCartWidget = true;
          this.cartService.getCount().subscribe(
            (count) => {
              this.cartCount = count;
            }
          );
        } else {
          this.showCartWidget = false;
        }
      }
    )
  }

  onCartClick() {
    this.router.navigate(['ordering/cart']);
  }
}