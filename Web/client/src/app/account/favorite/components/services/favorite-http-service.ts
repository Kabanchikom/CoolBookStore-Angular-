import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProductCatalogItemResponse } from 'src/app/product/services/http/types/productCatalog';

@Injectable({
  providedIn: 'root'
})
export class FavoriteHttpService {
  constructor(
    private http: HttpClient
  ) { }

  getFavorites(): Observable<IProductCatalogItemResponse> {
    const url = "assets/mocks/favorites.json";
    return this.http.get<IProductCatalogItemResponse>(url);
  }
}