import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import ICartLine from 'src/app/ordering/cart/types/ICartLine';
import { AccountService } from 'src/app/shared/account/services/account.service';
import HttpBase from 'src/app/shared/http/HttpBase';
import { ICartAddRequest, ICartUpdateRequest } from './types/cart';

@Injectable({
  providedIn: 'root'
})
export class CartHttpService extends HttpBase {
  baseUrl = this.apiUrl + '/cart';

  constructor(
    protected override http: HttpClient,
    private accountService: AccountService
  ) {
    super(http);
  }

  getCart(): Observable<ICartLine[]> {
    return this.http.get<ICartLine[]>(`${this.baseUrl}/getCart`);
  }

  getCount(): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/getCount`);
  }

  add(request: ICartAddRequest): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/add`, request);
  }

  addRange(request: ICartAddRequest[]) {
    return this.http.post<ICartLine[]>(`${this.baseUrl}/addRange`, request);
  }

  deleteLine(productId: string) {
    return this.http.delete(`${this.baseUrl}/delete/${productId}`);
  }

  update(id: string, request: ICartUpdateRequest) {
    return this.http.put(`${this.baseUrl}/update/${id}`, request);
  }

  clear() {
    this.http.delete(`${this.baseUrl}/clear`);
  }
}