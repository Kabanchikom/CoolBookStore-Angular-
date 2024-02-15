import { Component, Input, OnChanges, OnInit } from '@angular/core';
import IProductCatalogItem from '../../types/IProductCatalogItem';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { ProductCardBoxComponent } from '../product-card-box/product-card-box.component';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [ CommonModule, RouterModule, UiKitModule, ProductCardBoxComponent ],
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent {
  showProductModal = false;
  @Input() products?: IProductCatalogItem[];

  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) {}

  onCardClick(id: string) {
    this.showProductModal = true;
    this.router.navigate(['detail', id], { relativeTo: this.route })
  }

  onCloseClick() {
    this.showProductModal = false;
    this.router.navigate(['../'], { relativeTo: this.route })
  }
}