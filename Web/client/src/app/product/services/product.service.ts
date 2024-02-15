import { Injectable } from '@angular/core';
import { ProductHttpService } from './http/product-http.service';
import { Observable } from 'rxjs';
import { IProductCatalogItemsRequest, IProductCatalogItemResponse } from './http/types/productCatalog';
import IProductDetail from '../types/IProductDetail';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(
    private http: ProductHttpService
  ) {}

  getCatalogItems(request: IProductCatalogItemsRequest): Observable<IProductCatalogItemResponse> {
    return this.http.getCatalogItems(request);
  }

  getProductDetail(id: string): Observable<IProductDetail> {
    return this.http.getProductDetail(id);
  }
}