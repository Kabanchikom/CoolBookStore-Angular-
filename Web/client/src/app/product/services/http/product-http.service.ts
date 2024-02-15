import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProductCatalogItemResponse, IProductCatalogItemsRequest } from './types/productCatalog';
import HttpBase from 'src/app/shared/http/HttpBase';
import IProductCatalogItem from '../../types/IProductCatalogItem';
import IProductDetail from '../../types/IProductDetail';

@Injectable({
  providedIn: 'root'
})
export class ProductHttpService extends HttpBase {
  baseUrl = this.apiUrl + '/product';

  getCatalogItems(request: IProductCatalogItemsRequest): Observable<IProductCatalogItemResponse> {
    return this.http.post<IProductCatalogItemResponse>(`${this.baseUrl}/catalogItems`, request);
  }

  getProductDetail(id: string) {
    return this.http.get<IProductDetail>(`${this.baseUrl}/detail/${id}`);
  }
}