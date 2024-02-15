import { Component } from '@angular/core';
import IProductCatalogItem from 'src/app/product/types/IProductCatalogItem';
import { Gap } from 'src/app/ui-kit/shared';
import { FavoriteHttpService } from '../services/favorite-http-service';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { FavoriteCardComponent } from '../favorite-card/favorite-card.component';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [ CommonModule, UiKitModule, FavoriteCardComponent ],
  selector: 'app-favorite-list',
  templateUrl: './favorite-list.component.html',
  styleUrls: ['./favorite-list.component.scss']
})
export class FavoriteListComponent {
  Gap = Gap;
  favorites?: IProductCatalogItem[];
  total?: number;

  constructor(
    private http: FavoriteHttpService,
  ) {}

  ngOnInit(): void {
    this.http.getFavorites().subscribe(x => {
      this.favorites = x.products;
      this.total = x.total;
    });
  }
}
