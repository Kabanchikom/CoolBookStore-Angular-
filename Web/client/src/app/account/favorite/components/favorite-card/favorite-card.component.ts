import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import IProductCatalogItem from 'src/app/product/types/IProductCatalogItem';
import { ProductCardListItemComponent } from 'src/app/shared/product/components/thumbs/product-card-list-item/product-card-list-item.component';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';

@Component({
  standalone: true,
  imports: [ CommonModule, ProductCardListItemComponent, UiKitModule ],
  selector: 'app-favorite-card',
  templateUrl: './favorite-card.component.html',
  styleUrls: ['./favorite-card.component.scss']
})
export class FavoriteCardComponent {
  @Input() product?: IProductCatalogItem;
  @Output() onDeleteClick? = new EventEmitter();

  handleDelete() {
    alert('on delete');
  }
}
