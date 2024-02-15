import { Component, Input, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import IProductDetail from '../../types/IProductDetail';
import { ActivatedRoute, Params } from '@angular/router';
import { ProductCardBoxComponent } from '../../components/product-card-box/product-card-box.component';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';

@Component({
  standalone: true,
  imports: [ ProductCardBoxComponent, UiKitModule ],
  selector: 'app-product-detail-page',
  templateUrl: './product-detail-page.component.html',
  styleUrls: ['./product-detail-page.component.scss']
})
export class ProductDetailPageComponent implements OnInit {
  product?: IProductDetail;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params
    .subscribe(
      (params: Params) => {
        this.productService
          .getProductDetail(params['id'])
          .subscribe(
            (product: IProductDetail) => {
              this.product = product;
            }
          );
      }
    );

    this.productService
    .getProductDetail(this.route.snapshot.params['id'])
    .subscribe(
      (product: IProductDetail) => {
        this.product = product;
      }
    );
  }
}